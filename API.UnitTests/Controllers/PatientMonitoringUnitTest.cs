using AlertToCareAPI.Controllers;
using AlertToCareAPI.Repository;
using Entities;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace API.UnitTests
{
    public class PatientMonitoringUnitTest
    {
        private readonly Mock<IMonitoringRepository> _mockRepo;
        private readonly PatientMonitoringController _controller;
        public PatientMonitoringUnitTest()
        {
            _mockRepo = new Mock<IMonitoringRepository>();
            _controller = new PatientMonitoringController(_mockRepo.Object);
        }
        [Fact]
        public void WhenGetVitalsExecutesReturnsTypeVitals()
        {
               var result = _controller.GetVitals();
               Assert.IsType<Vitals[]>(result);
        }
      
        
    }
}
