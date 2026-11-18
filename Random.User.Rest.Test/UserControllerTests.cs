using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Random.User.Rest.Controllers;
using Random.User.Rest.Infrastructure;
using Random.User.Rest.Model;

namespace Random.User.Rest.Test;

[TestClass]
public class UserControllerTests
{
    private static (DataContext Context, UsersController Controller) CreateController()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var context = new DataContext(options);
        return (context, new UsersController(context, Options.Create(new ConfigurationOptions { MaxUsersLimit = 2 })));
    }

    [TestMethod]
    public async Task PagesAreOneBasedAndUseRequestedPageSize()
    {
        var (context, controller) = CreateController();
        using (context)
        {
            context.Users.AddRange(
                new Model.User { Name = "A", Email = "a@example.com", UserName = "a" },
                new Model.User { Name = "B", Email = "b@example.com", UserName = "b" },
                new Model.User { Name = "C", Email = "c@example.com", UserName = "c" });
            await context.SaveChangesAsync();

            var first = await controller.GetUsers(1, 1);
            var second = await controller.GetUsers(2, 1);
            Assert.AreEqual("A", ((IEnumerable<Model.User>)((OkObjectResult)first.Result!).Value!).Single().Name);
            Assert.AreEqual("B", ((IEnumerable<Model.User>)((OkObjectResult)second.Result!).Value!).Single().Name);
        }
    }

    [TestMethod]
    public async Task InvalidPageReturnsBadRequest()
    {
        var (context, controller) = CreateController();
        using (context)
        {
            var response = await controller.GetUsers(0);
            Assert.IsInstanceOfType<BadRequestObjectResult>(response.Result);
        }
    }

    [TestMethod]
    public async Task MissingPutAndDeleteReturnNotFound()
    {
        var (context, controller) = CreateController();
        using (context)
        {
            Assert.IsInstanceOfType<NotFoundResult>(await controller.PutUser(99, new Model.User { UserId = 99 }, default));
            Assert.IsInstanceOfType<NotFoundResult>(await controller.DeleteUser(99, default));
        }
    }
}
