const selectImage = document.querySelector('.select-image');
const inputFile = document.querySelector('#file');
const imgArea = document.querySelector('.img-area');
const scoresContainer = document.querySelector('.scores-container');
const submitBtton = document.getElementById('submit_button');
const loadingIcon = document.querySelector('.loading-icon');
const buttonText = document.querySelector('.button-text');





const myForm = document.getElementById("imgForm");




const show = (element) => {
	element.style.display = "block";
}

const hide = (element) => {
	element.style.display = "none";
}

const setImageSearchData = (data) => {
	sessionStorage.setItem("imageSearchData", JSON.stringify(data))
}


const getImageSearchData = () => {
	const data = sessionStorage.getItem('imageSearchData');
	if (!data)
		return null;

	return JSON.parse(data);
}


const renderImageSearchContent = (data) => {
	for (let i = 0; i < data.dists.length; i++) {
		const imageDiv = document.createElement('div');
		const imageDivOverlay = document.createElement('div');
		const dist = document.createElement('h2');
		dist.innerText = data.dists[i];

		imageDiv.classList.add('col-sm-4', 'col-md-3', 'col-lg-2', 'tz-gallery-div');
		imageDivOverlay.classList.add("tz-gallery-div-overlay");

		imageDivOverlay.appendChild(dist);

		const imageLink = document.createElement('a');
		imageLink.classList.add('lightbox');
		const imageName = data.imagesPaths[i]
		const artifactId = imageName.split("_")[0];
		imageLink.href = `/Artifacts/Details/${artifactId}`;  // Template literal for string interpolation
		
		const image = document.createElement('img');
		image.src = '/ArtifactImages/' + imageName;
		image.alt = "Park"; // Set a generic alt text for accessibility
		imageLink.appendChild(imageDivOverlay)
		imageLink.appendChild(image);
		imageDiv.appendChild(imageLink);
		scoresContainer.appendChild(imageDiv);
	}
}

document.addEventListener('DOMContentLoaded', (_) => {
	const searchData = getImageSearchData();
	if (searchData) {
		renderImageSearchContent(searchData);
	}
})

myForm.addEventListener("submit", (event) =>
{
	event.preventDefault()
	scoresContainer.innerHTML = '';
	const selectedImage = inputFile.files[0];

	if (!selectedImage)
	{

		return showErrorMessage(AlertMessages.should_choose_image_msg);

	}
	//submitBtton.classList.add("loading-button")
	show(loadingIcon);
	hide(buttonText)


	const formData = new FormData();
	formData.append("image", selectedImage);

	axios.post('api/IntelligentSearch/GetSimilarImages', formData).then(res =>
	{
		const scores = res.data.data;
		renderImageSearchContent(scores);
		setImageSearchData(scores);
		show(buttonText);
		hide(loadingIcon);


	}).catch(err => {
		show(buttonText);
		hide(loadingIcon);
		showErrorMessage();
	})

});


selectImage.addEventListener('click', function ()
{
	inputFile.click();
})

inputFile.addEventListener('change', function ()
{
	const image = this.files[0]
	if (image.size < 10000000)
	{
		const reader = new FileReader();
		reader.onload = () =>
		{
			const allImg = imgArea.querySelectorAll('img');
			allImg.forEach(item => item.remove());
			const imgUrl = reader.result;
			const img = document.createElement('img');
			//console.log("test ",imgArea.dataset.default)
			img.src = imgUrl;
			imgArea.appendChild(img);
			imgArea.classList.add('active');
			imgArea.dataset.img = image.name;
		}
		reader.readAsDataURL(image);
	} else
	{
		alert("Image size more than 2MB");
	}
})