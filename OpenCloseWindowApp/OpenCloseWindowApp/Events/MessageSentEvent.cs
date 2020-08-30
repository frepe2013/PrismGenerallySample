using System;
using System.Collections.Generic;
using System.Text;
using Prism.Events;

namespace OpenCloseWindowApp.Events
{
    class MessageSentEvent : PubSubEvent<string>
    {
    }
}
