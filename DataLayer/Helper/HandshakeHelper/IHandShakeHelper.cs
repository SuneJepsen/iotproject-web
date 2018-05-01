using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Domain;

namespace DataLayer.Helper.HandshakeHelper
{
    public interface IHandShakeHelper
    {
        DateTime GetHandShakeFloor();
        DateTime GetHandShakeDoor();

        void SaveHandShakeFloor(Handshake handshake);
        void SaveHandShakeDoor(Handshake handshake);
    }
}
