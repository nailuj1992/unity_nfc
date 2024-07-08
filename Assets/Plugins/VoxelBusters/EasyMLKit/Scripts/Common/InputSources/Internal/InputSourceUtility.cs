using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EasyMLKit.Internal
{
    public class InputSourceUtility
    {
        //Sometimes, InputImageSource may need some transformations for getting the final expected co-ordinates.
        //We need to provide a function in inputSource which can transform a point to the expected space.
        //For example, ArCamera internally uses a direct captured image from camera and getting bounds in this space
        //doesn't make any sense as the user doesn't know how it internally works. Also he considers the ArCamera frame
        //as the one he see's on Screen. So, we need a way to convert a point in the actual input ML receives and convert it to a point in the final expected
        //space of the user. If we give the bounded rects(if any) in screen space, he can cast rays accordingly.
        //The same rule applies to LiveCamera feed where we may capture a certain resolution image and pass it to ML engine. But the
        //User sees a different aspect corrected (any may be scale one too if the captured image from camera is low res compared to the screen resolution).
        //on screen.

        public static Vector2 TransformPointToUserSpace(Vector2 point, float displayWidth, float displayHeight, float originalInputImageWidth, float originalInputImageHeight, float originalInputImageRotation)
        {
            return TransformPointToUserSpace(point, displayWidth, displayHeight, originalInputImageWidth, originalInputImageHeight, originalInputImageRotation,
#if UNITY_ANDROID
                0f
#else
                originalInputImageRotation                
#endif
                );
        }
        public static Vector3 TransformPointToUserSpace(Vector2 point, float displayWidth, float displayHeight, float originalInputImageWidth, float originalInputImageHeight, float originalInputImageRotation, float rectRotation)
        {
            Vector2 originalInputSize = new Vector2(originalInputImageWidth, originalInputImageHeight);
            Vector2 currentInputSize = GetNewDimensions(originalInputSize.x, originalInputSize.y, originalInputImageRotation);
            Quaternion rotation = Quaternion.Euler(0, 0, rectRotation);
            Vector2 displaySize = new Vector2(displayWidth, displayHeight);

            Vector2 aspectSize = GetAspectFillSize(displaySize, currentInputSize);
            Vector2 scale = GetAspectFillScale(aspectSize, currentInputSize);
            Vector2 halfOffset = (displaySize - aspectSize) / 2;

            //Move to center of the input image first
            Matrix4x4 matrix = Matrix4x4.Translate(-GetNewDimensions(currentInputSize.x, currentInputSize.y, -rectRotation) / 2); //Android just needs scale alone. Transalation still in portrait mode as there was no rotation
            point = matrix.MultiplyPoint(point);

            //Apply the transform to fit it as Aspect fill
            matrix = Matrix4x4.TRS((aspectSize / 2) + halfOffset, rotation, scale);
            point = matrix.MultiplyPoint(point);

            return new Vector3(point.x, point.y, scale.x);//Returning scale as extra param
        }


        //Get the center
        //Transform to user space
            //Aspect Fill Size - CurrentInputSize
            //Aspect Fill Scale - Aspect Size
            //DisplaySize
            ///

            ///InputRotation and Rect Rotation
            ///

        public static Rect TransformRectToUserSpace(Rect rawRect, float displayWidth, float displayHeight, float originalInputImageWidth, float originalInputImageHeight, float originalInputImageRotation)
        {
#if UNITY_ANDROID
            float rectRotation = 0f; //For Android, the bounding box is already in proper orientation
#else
            float rectRotation = originalInputImageRotation;
#endif

            //First scale and rotate by translating
            Vector2 position = rawRect.center;

            Vector3 transformedPoint = TransformPointToUserSpace(position, displayWidth, displayHeight, originalInputImageWidth, originalInputImageHeight, originalInputImageRotation, rectRotation);

            position = transformedPoint;


            //This is because the input rect may be flipped when input rotation is specified
            Vector2 rectSize = GetNewDimensions(rawRect.width, rawRect.height, rectRotation);
            rectSize.x *= transformedPoint.z;
            rectSize.y *= transformedPoint.z;

            return new Rect(position - rectSize / 2, rectSize);
        }


        private static Vector2 GetAspectFillScale(Vector2 aspectSize, Vector2 inputSize)
        {
            return new Vector2(aspectSize.x / inputSize.x, aspectSize.y / inputSize.y);
        }

        private static Vector2 GetAspectFillSize(Vector2 displaySize, Vector2 inputSize)
        {
            float inputAspect = inputSize.x / inputSize.y;
            float displayAspect = displaySize.x / displaySize.y;

            Vector2 aspectFillSize;
            if (displayAspect > inputAspect)
            {
                aspectFillSize.x = displaySize.x;
                aspectFillSize.y = aspectFillSize.x / inputAspect;
            }
            else
            {
                aspectFillSize.y = displaySize.y;
                aspectFillSize.x = aspectFillSize.y * inputAspect;
            }

            return aspectFillSize;
        }

        private static Vector2 GetNewDimensions(float inputImageWidth, float inputImageHeight, float inputImageRotation)
        {
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, inputImageRotation));
            Vector3 newDimensions = rotationMatrix.MultiplyPoint(new Vector3(inputImageWidth, inputImageHeight));
            newDimensions.x = Mathf.Abs(newDimensions.x);
            newDimensions.y = Mathf.Abs(newDimensions.y);
            return newDimensions;
        }
    }
}
