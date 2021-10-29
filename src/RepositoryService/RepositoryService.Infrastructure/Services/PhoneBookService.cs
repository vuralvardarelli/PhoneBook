using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RepositoryService.Core.Entities;
using RepositoryService.Core.Models;
using RepositoryService.Infrastructure.Data;
using RepositoryService.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RepositoryService.Infrastructure.Services
{
    public class PhoneBookService : IPhoneBookService
    {
        private readonly PhonebookContext _context;
        private readonly ICacheService _cacheService;
        private readonly AppSettings _appSettings;
        private static object _lock = new object();

        public PhoneBookService(PhonebookContext context, ICacheService cacheService, AppSettings appSettings)
        {
            _context = context;
            _cacheService = cacheService;
            _appSettings = appSettings;
        }

        public async Task AddRecord(Record record)
        {
            await _context.AddAsync(record);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRecord(int recordId)
        {
            Record record = _context.Records.FirstOrDefault(x => x.RecordId == recordId);

            if (record == null)
                throw new ArgumentNullException("record");

            _context.Records.Remove(record);
            await _context.SaveChangesAsync();
        }

        public async Task AddContactInfo(int recordId, ContactInfo contactInfo)
        {
            Record record = _context.Records.Include("ContactInfos").FirstOrDefault(x => x.RecordId == recordId);

            if (record == null)
                throw new ArgumentNullException("record");

            if (record.ContactInfos == null)
                record.ContactInfos = new List<ContactInfo>();

            record.ContactInfos.Add(contactInfo);
            _context.Records.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveContactInfo(int contactInfoId)
        {
            ContactInfo contactInfo = _context.ContactInfos.FirstOrDefault(x => x.ContactInfoId == contactInfoId);

            if (contactInfo == null)
                throw new ArgumentNullException("ContactInfo");

            _context.ContactInfos.Remove(contactInfo);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Record>> GetRecords()
        {
            List<Record> records = null;

            if (await _cacheService.Any(_appSettings.RecordsCacheKey))
            {
                records = await _cacheService.Get<List<Record>>(_appSettings.RecordsCacheKey);
            }
            else
            {
                records = await _context.Records.Include("ContactInfos").ToListAsync();
                records = JsonConvert.DeserializeObject<List<Record>>(JsonConvert.SerializeObject(records));

                await _cacheService.AddWithExpire(_appSettings.RecordsCacheKey, records, _appSettings.RecordsCacheTimeoutAsSeconds);
            }

            return records;
        }
    }
}
