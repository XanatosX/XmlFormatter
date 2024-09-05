namespace XmlFormatter.Domain.PluginFeature;

/// <summary>
/// This class represents all the information of the plugin
/// </summary>
public class PluginInformation
{
    /// <summary>
    /// The name of the plugin
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The url to the project
    /// </summary>
    public string? ProjectUrl { get; }

    /// <summary>
    /// The description of the plugin
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// THe Markdown description for this plugin information
    /// </summary>
    public string MarkdownDescription { get; private set; }

    /// <summary>
    /// The author name of the plugin
    /// </summary>
    public string Author { get; }

    /// <summary>
    /// The url to the author
    /// </summary>
    public string? AuthorUrl { get; }

    /// <summary>
    /// The version of the plugin
    /// </summary>
    public Version Version { get; }

    /// <summary>
    /// Create a new plugin information instance
    /// </summary>
    /// <param name="name">Name of the plugin</param>
    /// <param name="description">Description of the plugin</param>
    /// <param name="author">Author name of the plugin</param>
    /// <param name="version">The version of the plugin</param>
    public PluginInformation(string name, string description, string author, Version version) : this(name, description, author, version, null)
    {
    }

    /// <summary>
    /// Create a new plugin information instance
    /// </summary>
    /// <param name="name">Name of the plugin</param>
    /// <param name="description">Description of the plugin</param>
    /// <param name="author">Author name of the plugin</param>
    /// <param name="version">The version of the plugin</param>
    /// <param name="authorUrl">The url with information about the author</param>
    public PluginInformation(string name, string description, string author, Version version, string? authorUrl) : this(name, description, author, version, authorUrl, null)
    {
    }

    /// <summary>
    /// Create a new plugin information instance
    /// </summary>
    /// <param name="name">Name of the plugin</param>
    /// <param name="description">Description of the plugin</param>
    /// <param name="author">Author name of the plugin</param>
    /// <param name="version">The version of the plugin</param>
    /// <param name="authorUrl">The url with information about the author</param>
    /// <param name="projectUrl">The url to the project</param>
    public PluginInformation(string name, string description, string author, Version version, string? authorUrl, string? projectUrl)
    {
        Name = name;
        Description = description;
        Author = author;
        Version = version;
        AuthorUrl = authorUrl;
        ProjectUrl = projectUrl;
        MarkdownDescription = string.Empty;
        SetMarkdownDescription(description);
    }

    public void SetMarkdownDescription(string description)
    {
        MarkdownDescription = description;
    }
}
