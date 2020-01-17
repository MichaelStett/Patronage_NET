namespace Patronage_NET.Web.Controllers.Help
{
    public interface IHelper
    {
        bool IsContentValid(string content);
        string GetNewPath(string path, string name, string content);
        int GetContentCap();
        int GetFileSizeCap();
        string GetPath();
    }
}
