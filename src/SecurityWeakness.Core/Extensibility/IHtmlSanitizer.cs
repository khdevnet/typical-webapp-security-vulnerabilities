namespace SecurityWeakness.Core.Extensibility
{
    public interface IHtmlSanitizer
    {
        string Sanitize(string html);

        string SanitizeHtml(string html, params string[] blackList);
    }
}
