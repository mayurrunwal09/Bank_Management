using Domain_Library.Models;
using Domain_Library.View_Model;
using Infra_Library._dbContext_main;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly MainDbContext _context;
        public TransactionController(MainDbContext context)
        {
            _context = context;
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromForm] TransactionInsertModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (transaction.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "Invalid deposit amount.");
                return BadRequest(ModelState);
            }

            var account = await _context.Accounts.FindAsync(transaction.AccountId);

            if (account == null)
            {
                return NotFound("Account not found.");
            }
            var depositTransaction = new Transaction
            {
                TransactionId = transaction.TransactionId,
                Amount = transaction.Amount,
                TransactionDate = DateTime.Now,
                AccountId = account.Id,
                UserId = transaction.UserId,
                TransactionTypeId = transaction.TransactionTypeId
            };

            account.Balance += depositTransaction.Amount;
            try
            {
                _context.Transactions.Add(depositTransaction);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok("Deposit successful.");
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromForm] TransactionInsertModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (transaction.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "Invalid withdrawal amount.");
                return BadRequest(ModelState);
            }
           // var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == transaction.AccountNumber);

            var account = await _context.Accounts.FindAsync(transaction.AccountId);
            
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            if (account.Balance < transaction.Amount)
            {
                ModelState.AddModelError("Amount", "Insufficient funds.");
                return BadRequest(ModelState);
            }


            var withdrawalTransaction = new Transaction
            {
                TransactionId = transaction.TransactionId,

                Amount = -transaction.Amount,
                TransactionDate = DateTime.Now,
                AccountId = account.Id,
                UserId = transaction.UserId,
                TransactionTypeId = transaction.TransactionTypeId
            };


            account.Balance += withdrawalTransaction.Amount;

            try
            {
                _context.Transactions.Add(withdrawalTransaction);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok("Withdrawal successful.");
        }

        [HttpGet("getAllTransactions")]
        public IActionResult GetAllTransactions()
        {
            var transactions = _context.Transactions.ToList();
            return Ok(transactions);
        }
    }
}
