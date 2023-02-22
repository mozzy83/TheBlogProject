let index = 0;

function AddTag() {
    var tagEntry = document.getElementById("TagEntry");

    //Use search function to help detect an error state
    let searchResult = search(tagEntry.value);
    if (searchResult != null) {
        //Trigger sweetalert for condition contained in searchResult var
        swalWithDarkButton.fire({
            html: `<span class='font-weight-bolder'>${searchResult.toUpperCase()}</span>`
        });
    }
    else {
        //create a new select option
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;
    }


    //clear TagEntry control
    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    let tagCount = 1;
    let tagList = document.getElementById("TagList");
    if (!tagList) return false;
    if (tagList.selectedIndex == -1) {
        swalWithDarkButton.fire({
            html: "<span class='font-weight-bolder'>CHOOSE A TAG BEFORE DELETING</span>"
        });
        return true;
    }

    while (tagCount > 0) {
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;
            --tagCount;
        }
        else
            tagCount = 0;
        index--;
    }
}

$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
})

//Look for the tagValues variable to see if it has data
if (tagValues != '') {
    let tagArray = tagValues.split(",");
    for (let loop = 0; loop < tagArray.length; loop++) {
        //load up or replace the options that we have
        ReplaceTag(tagArray[loop], loop);
        index++
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag)
    document.getElementById("TagList").options[index] = newOption;
}

//The Search function will detect either an empty or a duplicate tag
//and return an error string if error is detected
function search(str) {
    if (str == "") {
        return "Empty tags are not permitted";
    }

    var tagsEl = document.getElementById('TagList');
    if (tagsEl) {
        let options = tagsEl.options;
        for (let index = 0; index < options.length; index++) {
            if (options[index].value == str)
                return `The Tag #${str} was detected as a duplicate and is not permitted`
        }
    }

}

const swalWithDarkButton = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-danger btn-sm btn-block btn-outline-dark'
    },
    imageUrl: '/images/Oops.jpg',
    timer: 3250,
    buttonsStyling: false
})