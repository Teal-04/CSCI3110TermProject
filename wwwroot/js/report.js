// report.js

// Fetch and render the monthly report
async function loadReport(userId, year, month) {
    try {
        const resp = await fetch(
            `/api/ReportsApi?userId=${userId}&year=${year}&month=${month}`
        );
        const report = await resp.json();
        renderReport(report);
    } catch (err) {
        console.error('Failed to load report', err);
    }
}

// Render into a UL with id="report-list"
function renderReport({ totalIncome, totalExpense, summaries }) {
    const ul = document.getElementById('report-list');
    ul.innerHTML = `
    <li><strong>Total Income:</strong> $${totalIncome}</li>
    <li><strong>Total Expense:</strong> $${totalExpense}</li>
  `;
    summaries.forEach(s => {
        ul.innerHTML += `<li>${s.categoryName}: $${s.totalSpent}</li>`;
    });
}

// On page load, pull current month for userId=1 (hardcode or replace)
document.addEventListener('DOMContentLoaded', () => {
    const now = new Date();
    loadReport(1, now.getFullYear(), now.getMonth() + 1);
});
