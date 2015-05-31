﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMR.Common
{
    public static class Messages
    {
        /// <summary>
        /// Message to navigate PageSwitcher to other view
        /// </summary>
        public const string NavigateTo = "navigateTo";
        /// <summary>
        /// Update the Frontend
        /// </summary>
        public const string UpdateFrontend = "updateFrontend";
        /// Invokes sending the Front End Properties
        /// </summary>
        public const string PropertyListAdd = "propertyListAdd";
        /// <summary>
        /// Message from the input View who pipelines input
        /// </summary>
        public const string Input = "input";
        
    }
}
