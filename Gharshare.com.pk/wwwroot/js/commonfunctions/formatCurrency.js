function formatCurrency(value) {
    if (typeof value === 'number') {
        return value.toLocaleString('en-PK', { style: 'currency', currency: 'PKR', minimumFractionDigits: 0, maximumFractionDigits: 0 });
    } else {
        return '';
    }
}
