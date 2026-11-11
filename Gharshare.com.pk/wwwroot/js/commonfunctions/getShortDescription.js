function getShortDescription(description, number) {
    if (description != null) {
        if (description.length > number) {
            return description.substring(0, number);
        }
        else {
            return description;
        }
    }
    else
        return "";
    
}