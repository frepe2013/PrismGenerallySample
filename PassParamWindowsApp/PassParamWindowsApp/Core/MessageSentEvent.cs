using System;
using System.Collections.Generic;
using System.Text;
using Prism.Events;

namespace PassParamWindowsApp.Core
{
    class MessageSentEvent : PubSubEvent<string>
    {
    }
}
