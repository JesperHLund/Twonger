﻿    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    namespace SharedMessages
    {
        public class Tweet
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Specifies that the Id property is auto-generated by the database
            public int? Id { get; set; }
            public string Body { get; set; }
            public int UserID { get; set; }

            public Tweet(string body, int userID)
            {
                Body = body;
                UserID = userID;
            }

        }
    }
