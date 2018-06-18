namespace DSA.Problems.ToDo
{
    /// <summary>
    ///     Given a tree representation of an HTML parsed output, wherein every block is a node in the tree, find if two HTML docs contain the same text.
    /// </summary>
    /// <input>
    ///     struct Node {
    ///         string value;
    ///         bool isMetadata;
    ///         Node[] children;
    ///     }
    /// </input>
    /// <output>
    ///     Missing number 2
    /// </output>
    /// 
    /// <notes>
    ///     For eg, consider the two documents
    ///     
    ///     <html><head>sample</head><body>
    ///     Hello world
    ///     </body></html>
    ///     
    ///     Will be represented as: 
    ///     Node1: value sample | children: <body> | isMetadata: true
    ///     Node2: value: <body> | children: | isMetadata: true
    ///     Node3: value: null | children: Hello world | isMetadata: true
    ///     Node4: value Hello world | isMetadata: false
    ///     
    ///     And a second document:
    ///     <html><body>Hello world</body></html>
    ///     Both documents are equivalent since they contain the same text data.
    ///     
    ///     Extra: The case of both documents fitting in memory is trivial, since it is just walking this tree list, consolidating data and comparing.
    ///     As a follow up, solve the case where the whole documents may not be able to fit in memory.
    /// </notes>
    public class HtmlTree
    {
    }
}