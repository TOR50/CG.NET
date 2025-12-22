class Library
{
    int bookid;
    string title;

    private Dictionary<int, string> book = new Dictionary<int, string>();

    public string this[int bookid]
    {
        get {return book[bookid]; }
        set { book[bookid] = value; }
    }
    public string this[string title]
    {
        get
        {
            return book.FirstOrDefault(e => e.Value == title).Value;
        }
    }

}