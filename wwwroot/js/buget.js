// budget.js

// Fetch & render all budgets
async function loadBudgets() {
    try {
        const resp = await fetch('/api/BudgetApi');
        const list = await resp.json();
        renderBudgets(list);
    } catch (err) {
        console.error('Failed to load budgets', err);
    }
}

// Render into a UL (or table) with id="budget-list"
function renderBudgets(budgets) {
    const ul = document.getElementById('budget-list');
    ul.innerHTML = '';
    budgets.forEach(b => {
        const li = document.createElement('li');
        li.textContent = `Category #${b.categoryId}: $${b.amount}`;
        // Wire up edit/delete if you like:
        // li.appendChild(makeEditButton(b));
        ul.appendChild(li);
    });
}

// Create a new budget
async function addBudget(b) {
    try {
        await fetch('/api/BudgetApi', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(b)
        });
        loadBudgets();
    } catch (err) {
        console.error('Failed to add budget', err);
    }
}

// Update a budget
async function updateBudget(id, updatedB) {
    try {
        await fetch(`/api/BudgetApi/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(updatedB)
        });
        loadBudgets();
    } catch (err) {
        console.error('Failed to update budget', err);
    }
}

// Delete a budget
async function deleteBudget(id) {
    try {
        await fetch(`/api/BudgetApi/${id}`, { method: 'DELETE' });
        loadBudgets();
    } catch (err) {
        console.error('Failed to delete budget', err);
    }
}

// Auto-load on page ready
document.addEventListener('DOMContentLoaded', loadBudgets);
