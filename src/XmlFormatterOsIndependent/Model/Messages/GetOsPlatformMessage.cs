using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlFormatterOsIndependent.Model.Messages;
internal class GetOsPlatformMessage : RequestMessage<OperationSystemEnum>
{
}
