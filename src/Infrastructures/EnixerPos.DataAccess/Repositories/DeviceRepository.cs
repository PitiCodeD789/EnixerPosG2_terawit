using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DataContext _context;
        public DeviceRepository(DataContext context)
        {
            _context = context;
        }
        public DeviceEntity GetDeviceByImei(string imei)
        {
            try
            {
                return _context.Device.Where(x => x.Imei == imei).FirstOrDefault();
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
