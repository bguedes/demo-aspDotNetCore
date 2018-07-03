using Couchbase;
using Couchbase.Core;
using Couchbase.N1QL;
using emonthly_web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emonthly_web.Controllers
{
    public class ReportsController : Controller
    {
        private IBucket _bucket;

        public ReportsController()
        {
            _bucket = ClusterHelper.GetBucket("emonthly");
        }

        [HttpGet]
        [Route("api/reports")]
        public IActionResult GetAll()
        {
            var getAllN1QL = @"select report.*, META(report).id from emonthly report where report.type = 'report'";
            var query = QueryRequest.Create(getAllN1QL);
            query.ScanConsistency(ScanConsistency.RequestPlus);

            var result = _bucket.Query<Report>(query);
            return Ok(result.Rows);
        }
    }
}
