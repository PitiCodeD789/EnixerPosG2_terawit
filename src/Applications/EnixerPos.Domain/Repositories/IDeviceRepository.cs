using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.Entities;

namespace EnixerPos.Domain.Repositories
{
    public interface IDeviceRepository
    {
        DeviceEntity GetDeviceByImei(string imei);
    }
}
