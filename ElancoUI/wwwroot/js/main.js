
function toggleInvoiceModal() {
    if (document.querySelector(".modal-invoice").style.display === "block") {
        document.querySelector(".modal-invoice").style.display = "none";
    }
    else {
        document.querySelector(".modal-invoice").style.display = "block";
    }
}

async function setImageUsingStreaming(imageElementId, imageStream) {
    const arrayBuffer = await imageStream.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);

    document.getElementById(imageElementId).src = url;
}