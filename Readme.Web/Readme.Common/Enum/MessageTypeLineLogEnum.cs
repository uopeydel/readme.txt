using System;
using System.Collections.Generic;
using System.Text;

namespace Readme.Common.Enum
{
    /// <summary>
    /// use camel case cuz need to convert to string for check contain text in message status.
    /// </summary>
    public enum MessageTypeLineLogEnum
    {
        none = 0,
        text = 1,
        image = 2,
        video = 3,
        audio = 4,
        location = 5,
        sticker = 6,
        imagemap = 7,
        template = 8,
    }
}
