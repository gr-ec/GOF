using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOF.Structural
{
  /* Use carefully because it's hard not to violate SOLID (could turn into a God class).
   * Resume complex tasks by creating a simple API. It's safer to use when you need to create a good interface
   * between good code and bad code (ball of mud). */

  /// <summary>
  /// External complex API that will be summarized by facade.
  /// </summary>
  public class ExternalAPI
  {
    public class VideoRecorder
    {
      public void StardRecording()
      {
        Console.WriteLine("Start recording");
      }
      public string StopRecording()
      {
        Console.WriteLine("Stop recording and retrieve video");

        return "Video recorded";
      }
    }
    public class VideoCompressor
    {
      public void CompressVideo()
      {
        Console.WriteLine("Video compressed");
      }
    }
    public class VideoPersister
    {
      public void PersistVideoDB()
      {
        Console.WriteLine("Video persisted in database");
      }
    }

    public class VideoUploader
    {
      public void UploadVideo()
      {
        Console.WriteLine("Video uploaded");
      }
    }

    public class VideoNotifier
    {
      public void NotifyFriends()
      {
        Console.WriteLine("Friends notified");
      }
    }
  }
  
  /// <summary>
  /// Summarizes complex tasks. Warning: it's easy not to be SOLID friendly!
  /// </summary>
  public class Facade
  {
    public void ShortVideoDeploy()
    {
      //code below violate "D" in SOLID - it's better to use dependency injection
      var vr = new ExternalAPI.VideoRecorder();
      var vc = new ExternalAPI.VideoCompressor();
      var vp = new ExternalAPI.VideoPersister();
      var vu = new ExternalAPI.VideoUploader();
      var vn = new ExternalAPI.VideoNotifier();

      /* code below groups different tasks that relate to each other (if you are integrating
       * bad code - "ball of mud" - it's not so bad to do this). */
      vr.StardRecording();
      Thread.Sleep(5000);
      vc.CompressVideo();
      vr.StopRecording();
      vp.PersistVideoDB();
      vu.UploadVideo();
      vn.NotifyFriends();
    }
  }

  public class FacadeClient
  {
    /* just call the facade */

    public static void Run()
    {
      var f = new Facade();

      f.ShortVideoDeploy();
    }
  }
}
