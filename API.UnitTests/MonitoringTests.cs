using AlertToCareAPI.Controllers;
using AlertToCareAPI.Repositories;
using AlertToCareAPI.ICUDatabase.Entities;
using Moq;
using Xunit;

namespace API.UnitTests
{
    public class MonitoringTests
    {
        private readonly Mock<IMonitoringRepository> _mockRepo;
        private readonly PatientMonitoringController _controller;
        public MonitoringTests()
        {
            _mockRepo = new Mock<IMonitoringRepository>();
            _controller = new PatientMonitoringController(_mockRepo.Object);
        }
       /* [Fact]
        public void WhenGetVitalsExecutesReturnsTypeVitals()
        {
               var result = _controller.GetVitals();
               
               Assert.IsType<>(result);
        }
      
     */   
    }
}
