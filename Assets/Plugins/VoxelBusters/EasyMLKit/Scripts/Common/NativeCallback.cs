using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EasyMLKit.Internal
{
    public abstract class NativeCallbackBase : IDisposable
    {
        protected object Callback { get; set; }

        public virtual void Dispose()
        {
            Callback = null;
        }

        protected static void InvokeCallback(IntPtr tag, Action<NativeCallback> action)
        {
            Callback callback = () =>
            {
                var tagHandle = GCHandle.FromIntPtr(tag);
                var callbackController = (NativeCallback)tagHandle.Target;
                action(callbackController);
                callbackController.Dispose();
                tagHandle.Free();
            };

            callback();
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TDelegate<TParam1>(TParam1 param1);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TDelegate<TParam1, TParam2>(TParam1 param1, TParam2 param2);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TDelegate<TParam1, TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3);


    public class NativeCallback : NativeCallbackBase
    {
        public NativeCallbackData Get(Action callback)
        {
            Callback = callback;
            NativeCallbackData Data = new NativeCallbackData(MarshalUtility.GetIntPtr(this), InternalCallback);
            return Data;
        }

        public NativeCallbackData<TParam1> Get<TParam1>(Action<TParam1> callback)
        {
            Callback = callback;
            NativeCallbackData<TParam1> Data = new NativeCallbackData<TParam1>(MarshalUtility.GetIntPtr(this), InternalCallback);
            return Data;
        }

        public NativeCallbackData<TParam1, TParam2> Get<TParam1, TParam2>(Action<TParam1, TParam2> callback)
        {
            Callback = callback;
            NativeCallbackData<TParam1, TParam2> Data = new NativeCallbackData<TParam1, TParam2>(MarshalUtility.GetIntPtr(this), InternalCallback);
            return Data;
        }

        public NativeCallbackData<TParam1, TParam2, TParam3> Get<TParam1, TParam2, TParam3>(Action<TParam1, TParam2, TParam3> callback)
        {
            Callback = callback;
            NativeCallbackData<TParam1, TParam2, TParam3> Data = new NativeCallbackData<TParam1, TParam2, TParam3>(MarshalUtility.GetIntPtr(this), InternalCallback);
            return Data;
        }


        [MonoPInvokeCallback(typeof(object))] //MonoPInvokeCallback is "in name only" and does nothing. Used just as a tag for IL2CPP to identify its a native callback function.
        private static void InternalCallback(IntPtr tag)
        {
            InvokeCallback(tag, (NativeCallback callbackController) =>
            {
                Action callback = (Action)(callbackController.Callback);
                callback?.Invoke();
            });
        }

        [MonoPInvokeCallback(typeof(object))]
        private static void InternalCallback<TParam1>(IntPtr tag, TParam1 param1)
        {
            InvokeCallback(tag, (NativeCallback callbackController) =>
            {
                Action<TParam1> callback = (Action<TParam1>)callbackController.Callback;
                callback?.Invoke(param1);
            });
        }


        [MonoPInvokeCallback(typeof(object))]
        private static void InternalCallback<TParam1, TParam2>(IntPtr tag, TParam1 param1, TParam2 param2)
        {
            InvokeCallback(tag, (NativeCallback callbackController) =>
            {
                Action<TParam1, TParam2> callback = (Action<TParam1, TParam2>)callbackController.Callback;
                callback?.Invoke(param1, param2);
            });
        }

        [MonoPInvokeCallback(typeof(object))]
        private static void InternalCallback<TParam1, TParam2, TParam3>(IntPtr tag, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            InvokeCallback(tag, (NativeCallback callbackController) =>
            {
                Action<TParam1, TParam2, TParam3> callback = (Action<TParam1, TParam2, TParam3>)callbackController.Callback;
                callback?.Invoke(param1, param2, param3);
            });
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeCallbackData
    {
        public IntPtr CallbackController; //Tag on which we need to call Dispose for proper memory deallocation
        public Action<IntPtr> CallbackPtr;

        public NativeCallbackData(IntPtr callbackController, Action<IntPtr> callback)
        {
            CallbackController = callbackController;
            CallbackPtr = callback;
        }

        public NativeCallbackDataWrapper GetDataWrapper()
        {
            return new NativeCallbackDataWrapper(CallbackController, Marshal.GetFunctionPointerForDelegate(CallbackPtr));
        }
    }

    public struct NativeCallbackData<TParam1>
    {
        public IntPtr CallbackController; //Tag on which we need to call Dispose for proper memory deallocation
        public Action<IntPtr, TParam1> CallbackPtr;

        public NativeCallbackData(IntPtr callbackController, Action<IntPtr, TParam1> callback)
        {
            CallbackController = callbackController;
            CallbackPtr = callback;
        }

        public NativeCallbackDataWrapper GetDataWrapper()
        {
            return new NativeCallbackDataWrapper(CallbackController, Marshal.GetFunctionPointerForDelegate(CallbackPtr));
        }
    }

    public struct NativeCallbackData<TParam1, TParam2>
    {
        public IntPtr CallbackController; //Tag on which we need to call Dispose for proper memory deallocation
        public Action<IntPtr, TParam1, TParam2> CallbackPtr;

        public NativeCallbackData(IntPtr callbackController, Action<IntPtr, TParam1, TParam2> callback)
        {
            CallbackController = callbackController;
            CallbackPtr = callback;
        }

        public NativeCallbackDataWrapper GetDataWrapper()
        {
            return new NativeCallbackDataWrapper(CallbackController, Marshal.GetFunctionPointerForDelegate(CallbackPtr));
        }
    }

    public struct NativeCallbackData<TParam1, TParam2, TParam3>
    {
        public IntPtr CallbackController; //Tag on which we need to call Dispose for proper memory deallocation
        public Action<IntPtr, TParam1, TParam2, TParam3> CallbackPtr;

        public NativeCallbackData(IntPtr callbackController, Action<IntPtr, TParam1, TParam2, TParam3> callback)
        {
            CallbackController = callbackController;
            CallbackPtr = callback;
        }

        public NativeCallbackDataWrapper GetDataWrapper()
        {
            return new NativeCallbackDataWrapper(CallbackController, Marshal.GetFunctionPointerForDelegate(CallbackPtr));
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeCallbackDataWrapper
    {
        public IntPtr CallbackController;
        public IntPtr CallbackPtr;

        public NativeCallbackDataWrapper(IntPtr callbackController, IntPtr callbackPtr)
        {
            CallbackController = callbackController;
            CallbackPtr = callbackPtr;
        }
    }
}

