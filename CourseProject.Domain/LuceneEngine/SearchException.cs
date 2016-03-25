﻿using System;

namespace CourseProject.Domain.LuceneEngine
{
    /// <summary>
    /// Custom exception, used when specific search errors occurs
    /// </summary>
    public class SearchException:Exception
    {
        public SearchException(string message):base(message)
        {
            
        }
    }
}
