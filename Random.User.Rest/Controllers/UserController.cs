using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Random.User.Rest.Infrastructure;
using Random.User.Rest.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Random.User.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext context;
        public readonly ConfigurationOptions configuration;

        public UserController(DataContext context, ConfigurationOptions configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        #region GET

        /// <remarks>
        /// GET: api/User
        /// Query users in the database, max quantity is in appsettings.
        /// </remarks>
        /// <returns>
        /// A list of users, sort by name and paginated.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model.User>>> GetUsers(int page, int cant)
        {
            List<Model.User> u = await context.Users.Include(a => a.Location).ToListAsync();
            int maxUsersLimit = configuration.MaxUsersLimit;

            // set default quantity to 50 if not specified
            // limited to N registers defined in the appsettings config parameter "MaxUsersLimit"
            cant = (cant == 0) ? maxUsersLimit : (cant > maxUsersLimit) ? maxUsersLimit : cant;

            // first the data is sorted by name
            // default page is the first
            // take quantity 
            var list = u.OrderBy(x => x.Name).Skip(maxUsersLimit * page).Take(cant).ToList();

            // TO-DO: Finish the SortHelper algorithm
            // This is not a algorithm method !!
            SeekAndSetTheEldest(ref list);

            return list;

        }

        /// <remarks>
        /// GET: api/User/5
        /// Find and user in the database.
        /// </remarks>
        /// <returns>
        /// The user, if it does not exist return "NotFound".
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Model.User>> GetUser(int id)
        {
            var item = await context.Users.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        #endregion

        #region POST

        /// <remarks>
        /// POST: api/User
        /// Insert a new user in the database.
        /// </remarks>
        /// <returns>
        /// The created user.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<Model.User>> PostUser(Model.User item)
        {
            context.Users.Add(item);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = item.UserId }, item);
        }

        #endregion

        #region PUT

        /// <remarks>
        /// PUT: api/User/5
        /// Update a user in the database.
        /// </remarks>
        /// <returns>
        /// If sucessful return "NoContent".
        /// If the user does not exists, return "NotFound".
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, Model.User item)
        {
            if (id != item.UserId)
            {
                return BadRequest();
            }

            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region DELETE

        /// <remarks>
        /// DELETE: api/User/5
        /// Delete a user from the database.
        /// </remarks>
        /// <returns>
        /// If sucessful return "NoContent".
        /// If the user does not exists, return "NotFound".
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var item = await context.Users.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            context.Users.Remove(item);
            await context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Support Methods

        private static void SeekAndSetTheEldest(ref List<Model.User> u)
        {
            int eldestId = u.OrderByDescending(x => x.Age).Select(x => x.UserId).FirstOrDefault();
            var eldestUser = u.FirstOrDefault(x => x.UserId == eldestId);
            if (eldestUser != null) { eldestUser.IsTheEldest = true; }
        }

        #endregion
    }
}
