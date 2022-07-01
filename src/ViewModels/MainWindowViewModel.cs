using Microsoft.Toolkit.Mvvm.ComponentModel;
using Ookii.Dialogs.Wpf;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace Easy_Real_ESRGAN.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {

        public bool IsWorking { get; set; }
        private string enhanceModel;
        public string EnhanceModel { get => enhanceModel; set => SetProperty(ref (enhanceModel), value); }

        private string gpu_AccelerateMode;
       public string GPUAccelerateMode { get => gpu_AccelerateMode; set => SetProperty(ref (gpu_AccelerateMode), value); }

        private string inputPath;
        public string InputPath { get => inputPath; set => SetProperty(ref (inputPath), value); }

        private string outputPath;
        public string OutputPath { get => outputPath; set => SetProperty(ref (outputPath), value); }
        
        private string OutputName { get; set; }

        private bool isVboseModeOn;
        public bool IsVboesModeOn { get => isVboseModeOn; set => SetProperty(ref (isVboseModeOn), value); }

        private string picFormat;
        public string PicFormat { get => picFormat; set => SetProperty(ref (picFormat), value); }
        public ICommand FileCommand => new CustomCommand(DoSomething, CanDoSomething);
        private void DoSomething(object param)
        {
            VistaOpenFileDialog dialog = new VistaOpenFileDialog();
            string[] InputFormat = { "PNG", "JPEG", "JPG", "TIF", "TIFF", "BMP", "TGA" };
            if (param.ToString() == "Input")
            {


                // Construct filter string for open file dialog.
                var filter = "";
                var count = 1;
                foreach (var ext in InputFormat)
                {
                    filter += $"{ext} (*.{ext})|*.{ext}|";
                    count++;
                }
                dialog.Filter = filter + "All files (*.*)|*.*";
                dialog.FilterIndex = count;

                if ((bool)dialog.ShowDialog())
                {
                    InputPath = dialog.FileName;
                    OutputName = dialog.FileName.Substring(0, dialog.FileName.LastIndexOf("."));
                    OutputPath = Path.Combine(Path.GetDirectoryName(dialog.FileName), " ").TrimEnd();
                }
            }
            else
            {
                VistaFolderBrowserDialog folderBrowserDialog = new VistaFolderBrowserDialog();
                dialog.Title = "Please select output folder."; // This applies to the Vista style dialog only, not the old dialog.
                if ((bool)folderBrowserDialog.ShowDialog())
                {
                    OutputPath = Path.Combine(folderBrowserDialog.SelectedPath, " ").TrimEnd();
                }
            }
        }
        private bool CanDoSomething(object param)
        {
            return true;
        }

        public ICommand StartCommand => new CustomCommand(DoStartEnhance, CanDoStartEnhance);
        private void DoStartEnhance(object param)
        {


            string ToolPath = System.Environment.CurrentDirectory + "\\Assets\\realesrgan-ncnn-vulkan.exe";
            
            if (File.Exists(ToolPath) == false)
            {
                System.Windows.Forms.MessageBox.Show("未找到Real-ESRGAN");
            }
            else
            {
                try
                {
                    ProcessStartInfo processInfo = new ProcessStartInfo();
                    processInfo.FileName = ToolPath;
                    processInfo.RedirectStandardOutput = true;
                    if (IsVboesModeOn)
                    {
                        processInfo.Arguments = $"-i {InputPath} -o {OutputPath+OutputName+"_Enhanced" +PicFormat} -n {EnhanceModel} -g {GPUAccelerateMode} -f {PicFormat} -v";

                    }
                    else
                    {
                        processInfo.Arguments = $"-i {InputPath} -o {OutputPath + OutputName + "_" + EnhanceModel + PicFormat} -n {EnhanceModel} -g {GPUAccelerateMode} -f {PicFormat}";

                    }
                    processInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    Process process = Process.Start(processInfo);
                    IsWorking = true;
                    process.WaitForExitAsync();
                }
                catch
                {

                }
                finally
                {
                    IsWorking = true;
                }
            }
        }


        private bool CanDoStartEnhance(object param)
        {
            return true;
        }

    }
}
