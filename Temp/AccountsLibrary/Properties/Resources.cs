using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;


namespace AccountsLibrary.Properties
{
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	public class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				if (resourceMan == null)
				{
					ResourceManager resourceManager = (resourceMan = new ResourceManager("AccountsLibrary.Properties.Resources", typeof(Resources).Assembly));
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		public static Bitmap AshproMDI
		{
			get
			{
				object @object = ResourceManager.GetObject("AshproMDI", resourceCulture);
				return (Bitmap)@object;
			}
		}

		public static string AuthSecret => ResourceManager.GetString("AuthSecret", resourceCulture);

		public static Bitmap Background_Image
		{
			get
			{
				object @object = ResourceManager.GetObject("Background_Image", resourceCulture);
				return (Bitmap)@object;
			}
		}

		public static Bitmap Barcode
		{
			get
			{
				object @object = ResourceManager.GetObject("Barcode", resourceCulture);
				return (Bitmap)@object;
			}
		}

		public static string BasePath => ResourceManager.GetString("BasePath", resourceCulture);

		public static Bitmap full_logo_png
		{
			get
			{
				object @object = ResourceManager.GetObject("full logo png", resourceCulture);
				return (Bitmap)@object;
			}
		}

		public static Icon ico_logo
		{
			get
			{
				object @object = ResourceManager.GetObject("ico_logo", resourceCulture);
				return (Icon)@object;
			}
		}

		public static Icon ico_logo1
		{
			get
			{
				object @object = ResourceManager.GetObject("ico_logo1", resourceCulture);
				return (Icon)@object;
			}
		}

		public static Bitmap Image1
		{
			get
			{
				object @object = ResourceManager.GetObject("Image1", resourceCulture);
				return (Bitmap)@object;
			}
		}

		public static Bitmap Logo
		{
			get
			{
				object @object = ResourceManager.GetObject("Logo", resourceCulture);
				return (Bitmap)@object;
			}
		}

		public static Bitmap Logo_360
		{
			get
			{
				object @object = ResourceManager.GetObject("Logo_360", resourceCulture);
				return (Bitmap)@object;
			}
		}

		public static Bitmap QRCode
		{
			get
			{
				object @object = ResourceManager.GetObject("QRCode", resourceCulture);
				return (Bitmap)@object;
			}
		}

		//public static Bitmap Splash
		//{
		//	get
		//	{
		//		object @object = ResourceManager.GetObject("Splash", resourceCulture);
		//		return (Bitmap)@object;
		//	}
		//}

		//public static Bitmap Splash_360
		//{
		//	get
		//	{
		//		object @object = ResourceManager.GetObject("Splash_360", resourceCulture);
		//		return (Bitmap)@object;
		//	}
		//}

		//public static Bitmap Splash_360_1
		//{
		//	get
		//	{
		//		object @object = ResourceManager.GetObject("Splash_360_1", resourceCulture);
		//		return (Bitmap)@object;
		//	}
		//}

		//public static Bitmap wall
		//{
		//	get
		//	{
		//		object @object = ResourceManager.GetObject("wall", resourceCulture);
		//		return (Bitmap)@object;
		//	}
		//}

		//public static Bitmap wall_360
		//{
		//	get
		//	{
		//		object @object = ResourceManager.GetObject("wall_360", resourceCulture);
		//		return (Bitmap)@object;
		//	}
		//}

		//public static Bitmap wall_360_1
		//{
		//	get
		//	{
		//		object @object = ResourceManager.GetObject("wall_360_1", resourceCulture);
		//		return (Bitmap)@object;
		//	}
		//}

		//public static Bitmap wall_demo
		//{
		//	get
		//	{
		//		object @object = ResourceManager.GetObject("wall_demo", resourceCulture);
		//		return (Bitmap)@object;
		//	}
		//}

		internal Resources()
		{
		}
	}
}
