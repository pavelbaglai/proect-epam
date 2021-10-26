const REVIEW_INPUT_PLACE_HOLDER = "Write a comment";

var reviewTextBox = document.getElementById("review-input");
var buttonContainer = document.getElementById("review-button-container");
var newReviewContent = document.getElementById("new-review-content");
var newReviewRating = document.getElementById("new-review-rating");
var reviewErrorMessage = document.getElementById("review-error-message");
var starInputs = document.getElementsByName("rating");
var isRatingMarked = false;

function markRating() {
    isRatingMarked = true;
}

function loseFocus() {
    if (isReviewTextBoxEmpty()
        && !isRatingMarked
        && reviewErrorMessage.innerText === '') {

        reviewTextBox.innerText = REVIEW_INPUT_PLACE_HOLDER
        reviewTextBox.style.border = "1px solid lightgray";
        reviewTextBox.style.color = "darkgray";
        buttonContainer.style.display = "none";
        isRatingMarked = false;
    }
}

function isReviewTextBoxEmpty() {
    return reviewTextBox.innerText.trim() === '' || reviewTextBox.innerText === '\n';
}

function getFocus() {
    if (reviewTextBox.innerText === REVIEW_INPUT_PLACE_HOLDER) {
        reviewTextBox.innerText = "";
        reviewTextBox.style.border = "2px solid orange";
        reviewTextBox.style.borderRadius = "5px";
        reviewTextBox.style.color = "black";
        buttonContainer.style.display = "block";
    }
}

function changeText() {
    newReviewContent.value = reviewTextBox.innerText;
}

function onClick(rating) {
    newReviewRating.value = rating;
}

function validateReview() {
    reviewErrorMessage.innerText = '';

    var isValid = true;
    var errorMessage = '';
    if (isReviewTextBoxEmpty()) {
        errorMessage += "Please write a comment! ";
        isValid = false;
    }

    if (newReviewRating.value === '0') {
        errorMessage += "Please select a rating!";
        isValid = false;
    }

    if (!isValid) {
        reviewErrorMessage.innerText = errorMessage;
        reviewErrorMessage.display = "block";
    }
}

function isValidReview() {
    return reviewErrorMessage.innerText === '';
}

function isRatingFocused() {
    for (var i = 0; i < starInputs.length; i++) {
        if (starInputs[i].checked == true) {
            return true;
        }
    }
    return false;
}

function cancelReview() {
    if (confirm("Do you want to cancel review?")) {
        reviewTextBox.innerText = '';
        reviewErrorMessage.innerText = '';
        newReviewRating.value = '0';
        isRatingMarked = false;

        for (var i = 0; i < starInputs.length; i++) {
            if (starInputs[i].checked == true) {
                starInputs[i].checked = false;
                break;
            }
        }

        loseFocus();
    }
}

function hideDeleteButton(id, isHidden) {
    var isUserReview = document.getElementById("user-review-" + id).value;
    if (isUserReview === "True") {
        var reviewBox = document.getElementById("delete-button-" + id);
        reviewBox.hidden = isHidden;
    }
}
