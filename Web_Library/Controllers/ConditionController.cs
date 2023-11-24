using Domain_Library.Models;
using Infra_Library._dbContext_main;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Web_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConditionController : ControllerBase
    {
        private readonly MainDbContext _context;
        public ConditionController(MainDbContext context)
        {
            _context = context;
        }
        [HttpGet("UserDetailsByID")]
        public IActionResult UserDetailsByID(int userId, string fromDate, string toDate)
        {
            if (!DateTime.TryParseExact(fromDate, "dd-MM-yyyy", null, DateTimeStyles.None, out DateTime parsedFromDate)
                || !DateTime.TryParseExact(toDate, "dd-MM-yyyy", null, DateTimeStyles.None, out DateTime parsedToDate))
            {
                return BadRequest("Invalid date format. Please use dd-MM-yyyy format.");
            }

            var user = _context.Users
                .Include(u => u.Transactions)
                .ThenInclude(t => t.TransactionType)
                .Include(u => u.Accounts)
                .Where(u => u.UserId == userId)
                .Select(u => new
                {
                    u.UserName,
                    Deposits = u.Transactions
                        .Where(t => t.TransactionType.TransactionTypeName == "Deposit" &&
                                    t.TransactionDate.Date >= parsedFromDate.Date &&
                                    t.TransactionDate.Date <= parsedToDate.Date)
                        .Select(t => new
                        {
                            t.TransactionId,
                            t.Amount,
                            t.TransactionDate
                        })
                        .ToList(),
                    Withdrawals = u.Transactions
                        .Where(t => t.TransactionType.TransactionTypeName == "Withdrawal" &&
                                    t.TransactionDate.Date >= parsedFromDate.Date &&
                                    t.TransactionDate.Date <= parsedToDate.Date)
                        .Select(t => new
                        {
                            t.TransactionId,
                            t.Amount,
                            t.TransactionDate
                        })
                        .ToList(),
                    AllTransactions = u.Transactions
                        .Where(t => t.TransactionDate.Date >= parsedFromDate.Date &&
                                    t.TransactionDate.Date <= parsedToDate.Date)
                        .Select(t => new
                        {
                            t.TransactionId,
                            t.Amount,
                            t.TransactionDate
                        })
                        .ToList()
                })
                .FirstOrDefault();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }
      /*  [HttpGet("TransactionsByUsernameAndDateRange")]
        public IActionResult TransactionsByUsernameAndDateRange(string username, string startDate, string endDate)
        {
            if (!DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedStartDate)
                || !DateTime.TryParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedEndDate))
            {
                return BadRequest("Invalid date format. Please use dd-MM-yyyy.");
            }

            // Find the user by username from your data source (e.g., database)
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Retrieve transactions within the specified date range for the user
            var transactions = _context.Transactions.Include(e => e.TransactionType)
                .Where(t => t.User.UserName == user.UserName && t.TransactionDate.Date >= parsedStartDate.Date && t.TransactionDate.Date <= parsedEndDate.Date)
                .ToList();

            return Ok(transactions);
        }
*/

        [HttpGet("GetUserDetailsByNaame")]
        public IActionResult GetUserDetailsByNaame(string userName, string fromDate, string toDate)
        {
            if (!DateTime.TryParseExact(fromDate, "dd-MM-yyyy", null, DateTimeStyles.None, out DateTime parsedFromDate)
                || !DateTime.TryParseExact(toDate, "dd-MM-yyyy", null, DateTimeStyles.None, out DateTime parsedToDate))
            {
                return BadRequest("Invalid date format. Please use dd-MM-yyyy format.");
            }

            var user = _context.Users
                .Include(u => u.Transactions)
                .ThenInclude(t => t.TransactionType)
                .Include(u => u.Accounts)
                .Where(u => u.UserName == userName)
                .Select(u => new
                {
                    u.UserName,
                    Deposits = u.Transactions
                        .Where(t => t.TransactionType.TransactionTypeName == "Deposit" &&
                                    t.TransactionDate.Date >= parsedFromDate.Date &&
                                    t.TransactionDate.Date <= parsedToDate.Date)
                        .Select(t => new
                        {
                            t.TransactionId,
                            t.Amount,
                            t.TransactionDate
                        })
                        .ToList(),
                    Withdrawals = u.Transactions
                        .Where(t => t.TransactionType.TransactionTypeName == "Withdrawal" &&
                                    t.TransactionDate.Date >= parsedFromDate.Date &&
                                    t.TransactionDate.Date <= parsedToDate.Date)
                        .Select(t => new
                        {
                            t.TransactionId,
                            t.Amount,
                            t.TransactionDate
                        })
                        .ToList(),
                    AllTransactions = u.Transactions
                        .Where(t => t.TransactionDate.Date >= parsedFromDate.Date &&
                                    t.TransactionDate.Date <= parsedToDate.Date)
                        .Select(t => new
                        {
                            t.TransactionId,
                            t.Amount,
                            t.TransactionDate
                        })
                        .ToList()
                })
                .FirstOrDefault();

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Accounts)
                    .ThenInclude(a => a.AccountType)
                .Include(u => u.Transactions)
                    .ThenInclude(t => t.TransactionType)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound($"User with id {userId} not found.");
            }

            var accounts = user.Accounts.Select(a => new
            {
                User = user.UserName,
                AccountId = a.AccountId,
                Balance = a.Balance,
                AccountType = a.AccountType.AccountTypeName,
                AccountNumber = a.AccountNumber,
                Transactions = a.Transactions.Select(t => new
                {
                    TransactionId = t.TransactionId,
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate,
                    TransactionType = t.TransactionType.TransactionTypeName

                })
            });

            return Ok(accounts);
        }

        

        [HttpPost("Transfer")]
        public async Task<IActionResult> TransferAmount(int senderAccountId, int receiverAccountId, int amount)
        {
            var senderAccount = await _context.Accounts.FindAsync(senderAccountId);
            var receiverAccount = await _context.Accounts.FindAsync(receiverAccountId);

            if (senderAccount == null || receiverAccount == null)
            {
                return NotFound("Sender account or receiver account not found.");
            }

            if (senderAccount.Balance < amount)
            {
                return BadRequest("Insufficient funds in the sender account.");
            }

            // Perform the transfer
            senderAccount.Balance -= amount;
            receiverAccount.Balance += amount;

            // Create transaction records for sender and receiver accounts
            var senderTransaction = new Transaction
            {
                Amount = -amount,
                TransactionDate = DateTime.UtcNow,
                AccountId = senderAccountId,
                TransactionTypeId = Transaction.TransferTransactionTypeId, // Use the constant value for transfer transactions
                UserId = senderAccount.UserId
            };

            var receiverTransaction = new Transaction
            {
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                AccountId = receiverAccountId,
                TransactionTypeId = Transaction.TransferTransactionTypeId, // Use the constant value for transfer transactions
                UserId = receiverAccount.UserId
            };

            _context.Transactions.AddRange(senderTransaction, receiverTransaction);
            await _context.SaveChangesAsync();

            return Ok("Transfer successful.");
        }

        [HttpPost("TransferAmountByAccountNumber")]
        public async Task<IActionResult> TransferAmountByAccountNumber(string senderAccountNumber, string receiverAccountNumber, int amount)
        {
            var senderAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == senderAccountNumber);
            var receiverAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == receiverAccountNumber);

            if (senderAccount == null || receiverAccount == null)
            {
                return NotFound("Sender account or receiver account not found.");
            }

            if (senderAccount.Balance < amount)
            {
                return BadRequest("Insufficient funds in the sender account.");
            }

            // Perform the transfer
            senderAccount.Balance -= amount;
            receiverAccount.Balance += amount;

            // Create transaction records for sender and receiver accounts
            var senderTransaction = new Transaction
            {
                Amount = -amount,
                TransactionDate = DateTime.UtcNow,
                AccountId = senderAccount.Id,
                TransactionTypeId = Transaction.TransferTransactionTypeId, // Use the constant value for transfer transactions
                UserId = senderAccount.UserId
            };

            var receiverTransaction = new Transaction
            {
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                AccountId = receiverAccount.Id,
                TransactionTypeId = Transaction.TransferTransactionTypeId, // Use the constant value for transfer transactions
                UserId = receiverAccount.UserId
            };

            _context.Transactions.AddRange(senderTransaction, receiverTransaction);
            await _context.SaveChangesAsync();

            return Ok("Transfer successful.");
        }

    }
}
    

