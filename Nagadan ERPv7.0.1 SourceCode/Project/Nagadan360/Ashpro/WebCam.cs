using System.Windows.Forms;
using WebCam_Capture;

namespace Ashpro
{
	internal class WebCam
	{
		private WebCamCapture webcam;

		private PictureBox _FrameImage;

		private int FrameNumber = 30;

		public void InitializeWebCam(ref PictureBox ImageControl)
		{
			webcam = new WebCamCapture();
			webcam.FrameNumber = 0uL;
			webcam.TimeToCapture_milliseconds = FrameNumber;
			webcam.ImageCaptured += webcam_ImageCaptured;
			_FrameImage = ImageControl;
		}

		private void webcam_ImageCaptured(object source, WebcamEventArgs e)
		{
			_FrameImage.Image = e.WebCamImage;
		}

		public void Start()
		{
			webcam.TimeToCapture_milliseconds = FrameNumber;
			webcam.Start(0uL);
		}

		public void Stop()
		{
			webcam.Stop();
		}

		public void Continue()
		{
			webcam.TimeToCapture_milliseconds = FrameNumber;
			webcam.Start(webcam.FrameNumber);
		}

		public void ResolutionSetting()
		{
			webcam.Config();
		}

		public void AdvanceSetting()
		{
			webcam.Config2();
		}
	}
}
