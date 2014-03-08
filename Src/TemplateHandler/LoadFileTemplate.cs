using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Antlr.StringTemplate;
using System.Text;
using System.IO;
using System.Collections.Specialized;

/// <summary>
/// LoadFileHtm 的摘要说明
/// </summary>
public class LoadFileTemplate : StringTemplateLoader
{
    /// <summary>
    /// How are the files encoded (ascii, UTF8, ...)?  You might want to read
    /// UTF8 for example on a ascii machine.
    /// </summary>
    private Encoding encoding;

    private FileSystemWatcher filesWatcher;
    private HybridDictionary fileSet;

    public LoadFileTemplate()
        : this(null, Encoding.Default, true)
    {
    }

    public LoadFileTemplate(string locationRoot)
        : this(locationRoot, Encoding.Default, true)
    {
    }
    public LoadFileTemplate(string locationRoot, bool raiseExceptionForEmptyTemplate)
        : this(locationRoot, Encoding.Default, raiseExceptionForEmptyTemplate)
    {
    }

    public LoadFileTemplate(string locationRoot, Encoding encoding)
        : this(locationRoot, encoding, true)
    {
    }

    public LoadFileTemplate(string locationRoot, Encoding encoding, bool raiseExceptionForEmptyTemplate)
        : base(locationRoot, raiseExceptionForEmptyTemplate)
    {
        if ((locationRoot == null) || (locationRoot.Trim().Length == 0))
        {
            this.locationRoot = AppDomain.CurrentDomain.BaseDirectory;
        }
        this.encoding = encoding;
        fileSet = new HybridDictionary(true);
    }

    /// <summary>
    /// Determines if the specified template has changed.
    /// </summary>
    /// <param name="templateName">template name</param>
    /// <returns>True if the named template has changed</returns>
    public override bool HasChanged(string templateName)
    {
        //string templateLocation = Path.Combine(LocationRoot, GetLocationFromTemplateName(templateName));
        string templateLocation = string.Format("{0}/{1}", LocationRoot, GetLocationFromTemplateName(templateName)).Replace('\\', '/');
        object o = fileSet[templateLocation];
        if ((o != null))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Loads the contents of the named StringTemplate.
    /// </summary>
    /// <param name="templateName">Name of the StringTemplate to load</param>
    /// <returns>
    /// The contexts of the named StringTemplate or null if the template wasn't found
    /// </returns>
    /// <exception cref="TemplateLoadException">Thrown if error prevents successful template load</exception>
    protected override string InternalLoadTemplateContents(string templateName)
    {
        string templateText = null;
        string templateLocation = null;

        try
        {
            //templateLocation = Path.Combine(LocationRoot, GetLocationFromTemplateName(templateName));
            templateLocation = string.Format("{0}/{1}", LocationRoot, GetLocationFromTemplateName(templateName)).Replace('\\', '/');
            StreamReader br;
            try
            {
                br = new StreamReader(templateLocation, encoding);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (DirectoryNotFoundException)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new TemplateLoadException("Cannot open template file: " + templateLocation, ex);
            }

            try
            {
                templateText = br.ReadToEnd();
                if ((templateText != null) && (templateText.Length > 0))
                {
                    //templateText = templateText.Trim();

                    if (filesWatcher == null)
                    {
                        filesWatcher = new FileSystemWatcher(LocationRoot, "*.html");
                        //filesWatcher.InternalBufferSize *= 2;
                        filesWatcher.NotifyFilter =
                            NotifyFilters.LastWrite
                            | NotifyFilters.Attributes
                            | NotifyFilters.Security
                            | NotifyFilters.Size
                            | NotifyFilters.CreationTime
                            | NotifyFilters.DirectoryName
                            | NotifyFilters.FileName;
                        filesWatcher.IncludeSubdirectories = true;
                        filesWatcher.Changed += new FileSystemEventHandler(OnChanged);
                        filesWatcher.Deleted += new FileSystemEventHandler(OnChanged);
                        filesWatcher.Created += new FileSystemEventHandler(OnChanged);
                        filesWatcher.Renamed += new RenamedEventHandler(OnRenamed);
                        filesWatcher.EnableRaisingEvents = true;
                    }
                }
                fileSet.Remove(templateLocation);
            }
            finally
            {
                if (br != null) ((IDisposable)br).Dispose();
                br = null;
            }
        }
        catch (ArgumentException ex)
        {
            string message;
            if (templateText == null)
                message = string.Format("Invalid file character encoding: {0}", encoding);
            else
                message = string.Format("The location root '{0}' and/or the template name '{1}' is invalid.", LocationRoot, templateName);

            throw new TemplateLoadException(message, ex);
        }
        catch (IOException ex)
        {
            throw new TemplateLoadException("Cannot close template file: " + templateLocation, ex);
        }
        return templateText;
    }

    /// <summary>
    /// Returns the location that corresponds to the specified template name.
    /// </summary>
    /// <param name="templateName">template name</param>
    /// <returns>The corresponding template location or null</returns>
    public override string GetLocationFromTemplateName(string templateName)
    {
        return templateName + ".html";
    }

    /// <summary>
    /// Returns the template name that corresponds to the specified location.
    /// </summary>
    /// <param name="templateName">template location</param>
    /// <returns>The corresponding template name or null</returns>
    public override string GetTemplateNameFromLocation(string location)
    {
        //return Path.ChangeExtension(location, string.Empty);
        return Path.ChangeExtension(location, null);
    }

    #region FileSystemWatcher Events

    private void OnChanged(object source, FileSystemEventArgs e)
    {
        string fullpath = e.FullPath.Replace('\\', '/');
        fileSet[fullpath] = locationRoot;
    }

    private void OnRenamed(object source, RenamedEventArgs e)
    {
        string fullpath = e.FullPath.Replace('\\', '/');
        fileSet[fullpath] = locationRoot;

        fullpath = e.OldFullPath.Replace('\\', '/');
        fileSet[fullpath] = locationRoot;
    }

    #endregion
}

public class Link
{
    public string faqid;
    public string faqtitle;
}

public class User
{
    public string Name = "";
    public string Age = "";
}



public static class FileHelp
{
    public static string Read(string filename, string Dir)
    {

        filename = AppDomain.CurrentDomain.BaseDirectory + Dir + filename;
        using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
        {
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            return sr.ReadToEnd();
        }

    }
}