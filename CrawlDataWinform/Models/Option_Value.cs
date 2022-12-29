namespace Crawl_Data;

public class Option_Value
{
    public Option_Value()
    {
        Value = new List<string>();
    }
    public string Option { get; set; }
    public List<string> Value { get; set; }
}