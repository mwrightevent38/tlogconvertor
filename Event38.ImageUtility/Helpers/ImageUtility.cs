using Event38.ImageUtility._Classes;
using log4net;
using MissionPlanner;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Event38.Helpers
{
    public static class ImageUtilityHelper
    {

private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public static string tlogToCSV(string filepath){
            CurrentState.SpeedUnit = "m/s";
            CurrentState.DistanceUnit = "m";
            MAVLinkInterface proto = new MAVLinkInterface();

           OpenFileDialog openFileDialog1 = new OpenFileDialog();
            
           
              
               string LogFilePath;
              openFileDialog1.FileName = filepath;
                   
                    foreach (string logfile in openFileDialog1.FileNames)
                    {

                        using (MAVLinkInterface mine = new MAVLinkInterface())
                        {
                            try
                            {
                                mine.logplaybackfile = new BinaryReader(File.Open(logfile, FileMode.Open, FileAccess.Read, FileShare.Read));
                            }
                            catch (Exception ex) { log.Debug(ex.ToString()); }
                            mine.logreadmode = true;


                            mine.MAV.packets.Initialize(); // clear
                            
                            StreamWriter sw = new StreamWriter(Path.GetDirectoryName(logfile) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(logfile) + ".csv");

                            while (mine.logplaybackfile.BaseStream.Position < mine.logplaybackfile.BaseStream.Length)
                            {

                                byte[] packet = mine.readPacket();
                                string text = "";
                                mine.DebugPacket(packet, ref text, true, ",");

                                sw.Write(mine.lastlogread.ToString("yyyy-MM-ddTHH:mm:ss.fff") + "," + text);
                            }
                           
                            sw.Close();
                            
                           

                            mine.logreadmode = false;
                            mine.logplaybackfile.Close();
                            mine.logplaybackfile = null;
                            LogFilePath = (Path.GetDirectoryName(logfile) + Path.DirectorySeparatorChar + (Path.GetFileNameWithoutExtension(logfile) + ".csv"));
                       
                            return LogFilePath;
                        }
                    }



                    return null; 
            }
        public static IEnumerable<string> GetTLog(string Path)
        {
            //return Directory.GetFiles(Path, "*.JPG,*.tif", SearchOption.AllDirectories)
            //    .AsEnumerable();

            return Directory.EnumerateFiles(Path, "*.*", SearchOption.TopDirectoryOnly)
            .Where(s => s.ToLower().EndsWith(".tlog"));
        }

    } 
    
}

 