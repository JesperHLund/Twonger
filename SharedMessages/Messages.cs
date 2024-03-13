namespace SharedMessages
{
    
    public class TweetMessage
    {
        //Can't predefine it as a tweet, as C# complains about different datatype then
        //Therefore using Object, as it should be able to handle any object type.
        public Object tweet { get; set; }

    }

    public class ProfileMessage
    {

        //This may need to be changed.
        public string Message { get; set; }
    }


}
