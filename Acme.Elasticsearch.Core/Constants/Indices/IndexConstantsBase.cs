namespace Acme.Elasticsearch.Core.Constants.Indices;

public abstract class IndexConstantsBase
{
    protected abstract string BaseIndexName { get; }

    private const string VersionSuffix = "-v";

    public string GetTemplateName() 
        => $"{BaseIndexName}-template";
    
    public string GetTemplatePattern() 
        => $"{BaseIndexName}*";
    
    public string GetVersionedIndexName(int version) 
        => $"{BaseIndexName}{VersionSuffix}{version}";

    public string GetVersionedAlias(int version) 
        => $"{BaseIndexName}{VersionSuffix}{version}";

    public string GetSearchAlias() 
        => BaseIndexName;
}