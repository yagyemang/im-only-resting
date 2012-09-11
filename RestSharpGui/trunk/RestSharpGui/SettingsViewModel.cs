﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Swensen.RestSharpGui.Properties;
using System.IO;
using System.Windows.Forms.Design;
using System.Drawing.Design;

namespace Swensen.RestSharpGui {

    //possible attributes
    //[Category("Test")]
    //[DisplayName("Test Property")]
    //[Description("This is the description that shows up")]

    /// <summary>
    /// View model for Settings to bind to the property grid.
    /// </summary>
    public class SettingsViewModel {
        private readonly Settings settings;

        public SettingsViewModel(Settings settings) {
            this.settings = settings;            
        }

        //[Browsable(false)]
        private Tuple<string, string> lastValidationError = Tuple.Create("","");

        /// <summary>
        /// Return the last validation error, or Tuple.Create("","") if none, and reset the last validation error to Tuple.Create("","")
        /// </summary>
        public Tuple<string, string> DequeueLastValidationError() {
            var temp = lastValidationError;
            lastValidationError = Tuple.Create("","");
            return temp;
        }

        //possible have PeakLastValidationError

        [Category("Request")]
        [DisplayName("Save Request File Dialog Folder")]
        [Description("The default folder set for the Save Request file dialog. This location gets overwritten automatically whenever a request is saved to a different location.")]
        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string SaveRequestFileDialogFolder {
            get { return settings.SaveRequestFileDialogFolder; }
            set { 
                if (!String.IsNullOrWhiteSpace(value) && !Directory.Exists(value))
                    lastValidationError = Tuple.Create("SaveRequestFileDialogFolder", "Specified directory does not exist");
                else
                    settings.SaveRequestFileDialogFolder = value; 
            }
        }

        [Category("Request")]
        [DisplayName("Default Request File Path")]
        [Description("The path for the default request file loaded upon startup.")]
        [EditorAttribute(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string DefaultRequestFilePath {
            get { return settings.DefaultRequestFilePath; }
            set {
                if (!String.IsNullOrWhiteSpace(value) && !File.Exists(value))
                    lastValidationError = Tuple.Create("DefaultRequestFilePath", "Specified file does not exist");
                else
                    settings.DefaultRequestFilePath = value;
            }
        }

        [Category("Request")]
        [DisplayName("Default Request Content-Type")]
        [Description("The default request Content-Type used when none is otherwise explicitly specified.")]
        public string DefaultRequestContentType {
            get { return settings.DefaultRequestContentType; }
            set { settings.DefaultRequestContentType = value; }
        }

        [Category("Request")]
        [DisplayName("Proxy Server")]
        [Description("Proxy server used by requests. If blank, no proxy server is used.")]
        public string ProxyServer {
            get { return settings.ProxyServer; }
            set { settings.ProxyServer = value; }
        }

        [Category("Response")]
        [DisplayName("Export Response Body File Dialog Folder")]
        [Description("The default folder set for the Export Response Body file dialog. This location gets overwritten automatically whenever a request body is saved to a different location.")]
        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string ExportResponseFileDialogFolder {
            get { return settings.ExportResponseFileDialogFolder; }
            set {
                if (!String.IsNullOrWhiteSpace(value) && !Directory.Exists(value))
                    lastValidationError = Tuple.Create("ExportResponseFileDialogFolder", "Specified directory does not exist");
                else
                    settings.ExportResponseFileDialogFolder = value;
            }
        }
    }
}
