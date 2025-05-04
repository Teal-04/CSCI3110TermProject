// transaction.js

// Fetch & render all transactions
async function loadTransactions() {
    try {
        const resp = await fetch('/api/TransactionsApi');
        const list = await resp.json();
        renderTransactions(list);
    } catch (err) {
        console.error('Failed to load transactions', err);
    }
}

// Render into a UL (or table) with id="tx-list"
function renderTransactions(transactions) {
    const ul = document.getElementById('tx-list');
    ul.innerHTML = '';
    transactions.forEach(tx => {
        const li = document.createElement('li');
        li.textContent = `${new Date(tx.date).toLocaleDateString()} – ${tx.type} – $${tx.amount} (${tx.categoryId})`;
        // You can add Edit/Delete buttons here and wire them to updateTransaction() / deleteTransaction()
        ul.appendChild(li);
    });
}

// Create a new transaction
async function addTransaction(tx) {
    try {
        await fetch('/api/TransactionsApi', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(tx)
        });
        loadTransactions();
    } catch (err) {
        console.error('Failed to add transaction', err);
    }
}

// Update an existing transaction
async function updateTransaction(id, updatedTx) {
    try {
        await fetch(`/api/TransactionsApi/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(updatedTx)
        });
        loadTransactions();
    } catch (err) {
        console.error('Failed to update transaction', err);
    }
}

// Delete a transaction
async function deleteTransaction(id) {
    try {
        await fetch(`/api/TransactionsApi/${id}`, { method: 'DELETE' });
        loadTransactions();
    } catch (err) {
        console.error('Failed to delete transaction', err);
    }
}

// Auto-load on page ready
document.addEventListener('DOMContentLoaded', loadTransactions);
