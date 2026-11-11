function formatDateToDDMMYY(datedb) {
    //debugger;
    const date = new Date(datedb); // Parse the date string into a Date object
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0'); // Month is 0-based, so add 1.
    const year = date.getFullYear(); // get full year.

    return `${day}/${month}/${year}`;
}