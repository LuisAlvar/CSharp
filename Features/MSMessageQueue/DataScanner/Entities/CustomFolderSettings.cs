﻿using System.Text.Json.Serialization;

namespace DataScanner.Entities
{
  public class CustomFolderSettings
  {
    /// <summary>Unique identifier of the combination File type/folder.
    /// Arbitrary number (for instance 001, 002, and so on)</summary>
    public string FolderId { get; set; } = null!;

    /// <summary>If TRUE: the file type and folder will be monitored</summary>
    public bool FolderEnabled { get; set; }
    /// <summary>Description of the type of files and folder location –
    /// Just for documentation purpose</summary>
    public string FolderDescription { get; set; } = null!;

    /// <summary>Filter to select the type of files to be monitored.
    /// (Examples: *.shp, *.*, Project00*.zip)</summary>
    public string FolderFilter { get; set; } = null!;

    /// <summary>Full path to be monitored
    /// (i.e.: D:\files\projects\shapes\ )</summary>
    public string FolderPath { get; set; } = null!;

    /// <summary>If TRUE: the folder and its subfolders will be monitored</summary>
    public bool FolderIncludeSub { get; set; }

    /// <summary>Specifies the command or action to be executed
    /// after an event has raised</summary>
    public string ExecutableFile { get; set; } = null!;

    /// <summary>List of arguments to be passed to the executable file</summary>
    public string ExecutableArguments { get; set; } = null!;
  }
}
