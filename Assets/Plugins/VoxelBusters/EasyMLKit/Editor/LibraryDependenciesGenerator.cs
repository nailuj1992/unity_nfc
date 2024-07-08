#if UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
using UnityEditor;
using System.Collections.Generic;
using System.Xml;
using System;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit.Editor
{
	/// <summary>
	/// Play-Services Dependencies for Cross Platform Easy ML Kit.
	/// </summary>
	[InitializeOnLoad]
	public class LibraryDependenciesGenerator
	{
		/// <summary>
		/// This is used to create a settings file
		/// which contains the dependencies specific to your plugin.
		/// </summary>
		private static readonly string DependencyName = "EasyMLKitDependencies.xml";


		/// <summary>
		/// Initializes static members of the <see cref="AndroidLibraryDependenciesGenerator"/> class.
		/// </summary>
		static LibraryDependenciesGenerator()
		{
			EditorApplication.update -= Update;
			EditorApplication.update += Update;
			EditorPrefs.SetBool("refresh-feature-dependencies", true);
		}

		public static bool CreateLibraryDependencies()
        {
			return CreateLibraryDependenciesInternal(IOServices.CombinePath(EasyMLKitPackageLayout.EditorExtrasPath, DependencyName));
		}

		private static void Update()
        {
			if (!EditorPrefs.HasKey("refresh-feature-dependencies") || EditorPrefs.GetBool("refresh-feature-dependencies")) // TODO: Remove this static key and have a callback system to get triggered when feature selection happens.
            {
				if (CreateLibraryDependencies())
				{
					EditorPrefs.SetBool("refresh-feature-dependencies", false);
				}
			}
        }

		private static bool CreateLibraryDependenciesInternal(string path)
		{
			if(!EasyMLKitSettings.IsConfigured())
            {
				return false;
            }

			// Settings
			XmlWriterSettings xmlSettings 	= new XmlWriterSettings();
			xmlSettings.Encoding			= new System.Text.UTF8Encoding(true);
			xmlSettings.ConformanceLevel	= ConformanceLevel.Document;
			xmlSettings.Indent 				= true;
			xmlSettings.NewLineOnAttributes	= true;
			xmlSettings.IndentChars			= "\t";

			//Check if settings exist


			// Generate and write dependecies
			using (XmlWriter xmlWriter = XmlWriter.Create(path, xmlSettings))
			{
				xmlWriter.WriteStartDocument();
				{
					xmlWriter.WriteComment("DONT MODIFY HERE. AUTO GENERATED DEPENDENCIES FROM AndroidLibraryDependenciesGenerator.cs [Cross Platform Easy ML Kit");

					xmlWriter.WriteStartElement("dependencies");
					{
						WriteIOSDependencies(xmlWriter);
						WriteAndroidDependencies(xmlWriter);

						xmlWriter.WriteEndElement();
					}
				}
				xmlWriter.WriteEndDocument();
			}

			return true;
		}

		private static void WriteIOSDependencies(XmlWriter xmlWriter)
		{
			xmlWriter.WriteStartElement("iosPods");

			if (EasyMLKitSettings.Instance.BarcodeScannerSettings.IsEnabled)
			{
				xmlWriter.WriteComment("Dependency added for using Barcode scanning");
				AddIosDependency(xmlWriter, "GoogleMLKit/BarcodeScanning");
			}

			if (EasyMLKitSettings.Instance.TextRecognizerSettings.IsEnabled)
			{
				xmlWriter.WriteComment("Dependency added for using Text Recognizer scanning");
				AddIosDependency(xmlWriter, "GoogleMLKit/TextRecognition");


				if (EasyMLKitSettings.Instance.TextRecognizerSettings.NeedsChineseLanguagesRecognition)
				{
					AddIosDependency(xmlWriter, "GoogleMLKit/TextRecognitionChinese");
				}

				if (EasyMLKitSettings.Instance.TextRecognizerSettings.NeedsDevanagariLanguagesRecognition)
				{
					AddIosDependency(xmlWriter, "GoogleMLKit/TextRecognitionDevanagari");
				}

				if (EasyMLKitSettings.Instance.TextRecognizerSettings.NeedsJapaneseLanguagesRecognition)
				{
					AddIosDependency(xmlWriter, "GoogleMLKit/TextRecognitionJapanese");
				}

				if (EasyMLKitSettings.Instance.TextRecognizerSettings.NeedsKoreanLanguagesRecognition)
				{
					AddIosDependency(xmlWriter, "GoogleMLKit/TextRecognitionKorean");
				}
			}

			if (EasyMLKitSettings.Instance.DigitalInkRecognizerSettings.IsEnabled)
			{
				xmlWriter.WriteComment("Dependency added for using Digital Ink Recognizer");
				AddIosDependency(xmlWriter, "GoogleMLKit/DigitalInkRecognition");
			}

			xmlWriter.WriteEndElement();
		}

		private static void WriteAndroidDependencies(XmlWriter xmlWriter)
		{
				xmlWriter.WriteStartElement("androidPackages");
				{
					xmlWriter.WriteComment("Dependency added for using Barcode Scanner");

					//For live camera input
					AddAndroidDependency(xmlWriter, "androidx.camera", "camera-camera2", "1.1.0-beta02");
					AddAndroidDependency(xmlWriter, "androidx.camera", "camera-lifecycle", "1.1.0-beta02");
					AddAndroidDependency(xmlWriter, "androidx.camera", "camera-view", "1.1.0-beta02");
					AddAndroidDependency(xmlWriter, "androidx.camera", "camera-core", "1.1.0-beta02");

					//Barcode scanner
					if (EasyMLKitSettings.Instance.BarcodeScannerSettings.IsEnabled)
					{
						AddAndroidDependency(xmlWriter, "com.google.mlkit", "barcode-scanning", "[17.0.2]");
						AddAndroidDependency(xmlWriter, "com.google.android.gms", "play-services-mlkit-barcode-scanning", "[18.0.0]");
					}

					//Object detector and tracker
					if (EasyMLKitSettings.Instance.ObjectDetectorAndTrackerSettings.IsEnabled)
					{
						AddAndroidDependency(xmlWriter, "com.google.mlkit", "object-detection", "[17.0.0]");
						AddAndroidDependency(xmlWriter, "com.google.mlkit", "object-detection-custom", "17.0.0");
						AddAndroidDependency(xmlWriter, "com.google.android.gms", "play-services-tasks", "18.0.1");
					}

					//Text Recognition
					if (EasyMLKitSettings.Instance.TextRecognizerSettings.IsEnabled)
					{
						string version = "16.0.0-beta4";

						//Adding latin by default
						AddAndroidDependency(xmlWriter, "com.google.mlkit", "text-recognition", version);

						if (EasyMLKitSettings.Instance.TextRecognizerSettings.NeedsChineseLanguagesRecognition)
						{
							AddAndroidDependency(xmlWriter, "com.google.mlkit", "text-recognition-chinese", version);
						}

						if (EasyMLKitSettings.Instance.TextRecognizerSettings.NeedsDevanagariLanguagesRecognition)
						{
							AddAndroidDependency(xmlWriter, "com.google.mlkit", "text-recognition-devanagari", version);
						}

						if (EasyMLKitSettings.Instance.TextRecognizerSettings.NeedsJapaneseLanguagesRecognition)
						{
							AddAndroidDependency(xmlWriter, "com.google.mlkit", "text-recognition-japanese", version);
						}

						if (EasyMLKitSettings.Instance.TextRecognizerSettings.NeedsKoreanLanguagesRecognition)
						{
							AddAndroidDependency(xmlWriter, "com.google.mlkit", "text-recognition-korean", version);
						}
					}

					//Face Detection
					if (EasyMLKitSettings.Instance.FaceDetectorSettings.IsEnabled)
					{
						string version = "16.1.5";

						AddAndroidDependency(xmlWriter, "com.google.mlkit", "face-detection", version);
					}

					if (EasyMLKitSettings.Instance.DigitalInkRecognizerSettings.IsEnabled)
					{
						string version = "18.1.0";
						AddAndroidDependency(xmlWriter, "com.google.mlkit", "digital-ink-recognition", version);
					}

					AddAndroidDependency(xmlWriter, "androidx.core", "core", "1.5+");
					AddAndroidDependency(xmlWriter, "androidx.lifecycle", "lifecycle-runtime", "2.4+");
					AddAndroidDependency(xmlWriter, "androidx.lifecycle", "lifecycle-extensions", "2.2+");


				//Fix for older Unity versions which doesn't support Java 11
#if !UNITY_2022_2_OR_NEWER
				AddAndroidDependency(xmlWriter, "androidx.annotation", "annotation-experimental", "[1.2.0]");//Force 1.2.0 as 1.3.0 compiled with java 11.
#endif
				}
				xmlWriter.WriteEndElement();
		}


		private static void AddIosDependency(XmlWriter xmlWriter, string artifact)
        {
			xmlWriter.WriteStartElement("iosPod");
			{
				xmlWriter.WriteAttributeString("name", artifact);

			}
			xmlWriter.WriteEndElement();
		}


		private static void AddAndroidDependency(XmlWriter xmlWriter, string group, string artifact, string version, string comment = null)
        {
			if(comment != null)
				xmlWriter.WriteComment(comment);

			AndroidDependency dependency = new AndroidDependency(group, artifact, version);
			WritePackageDependency(xmlWriter, dependency);
		}


		private static void WritePackageDependency(XmlWriter xmlWriter, AndroidDependency dependency)
		{
			xmlWriter.WriteStartElement ("androidPackage");
			{
				xmlWriter.WriteAttributeString ("spec", String.Format("{0}:{1}:{2}", dependency.Group, dependency.Artifact, dependency.Version));

				List<string> packageIDs = dependency.PackageIDs;

				if (packageIDs != null)
				{
					xmlWriter.WriteStartElement ("androidSdkPackageIds");
					{
						foreach(string _each in packageIDs)
						{
							xmlWriter.WriteStartElement ("androidSdkPackageId");
							{
								xmlWriter.WriteString (_each);
							}
							xmlWriter.WriteEndElement ();
						}
					}
					xmlWriter.WriteEndElement ();
				}

			}
			xmlWriter.WriteEndElement ();
		}
	}

	public class AndroidDependency
	{
        private readonly string m_group;
		private readonly string m_artifact;
		private readonly string m_version;

		private	List<string>	m_packageIDs;


		public string Group
		{
			get
			{
				return m_group;
			}
		}

		public string Artifact
		{
			get
			{
				return m_artifact;
			}
		}

		public string Version
		{
			get
			{
				return m_version;
			}
		}

		public List<string> PackageIDs
		{
			get
			{
				return m_packageIDs;
			}
		}

		public AndroidDependency(string _group, string _artifact, string _version)
		{
			m_group = _group;
			m_artifact = _artifact;
			m_version = _version;
		}

		public void AddPackageID(string _packageID)
		{
			if (m_packageIDs == null)
				m_packageIDs = new List<string>();


			m_packageIDs.Add(_packageID);
		}
	}
}
#endif
