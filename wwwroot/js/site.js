photoIDList = [];
getalert = () => {
    alert("Yöneticilerle iletişime geçin.");
}
exitClick = () => {
    $.get("logout");
    location.reload();
}
menubarClicked = () => {
    var element = document.getElementById("menubar");
    element.classList.add("bigMenu");
    var elementTwo = document.getElementById("sidenav");
    elementTwo.classList.add("smallNav");
    var elementThree = document.getElementById("close");
    elementThree.classList.add("closeOpen");
}
closebarClicked = () => {
    var element = document.getElementById("menubar");
    element.classList.remove("bigMenu");
    var elementTwo = document.getElementById("sidenav");
    elementTwo.classList.remove("smallNav");
    var elementThree = document.getElementById("close");
    elementThree.classList.remove("closeOpen");
}
closeAccount = (id) => {
    $.post(
        "users/disableuser",
        {userID: id},
        (data, status) => {
            if(data == "success")
            {
                location.reload();
            }
            else
            {
                alert("Bir sorun oluştu.");
            }
        }
    );
}
photoClicked = (classNumber, id) => {
    let control;
    photoIDList.find((number) => {
        if(number == id)
        {
            control = false;
        }
    })
    if(control || control == undefined)
    {
        photoIDList.push(id);
        document.getElementsByClassName("image")[classNumber].classList.add("imageOpacity");
        document.getElementsByClassName("tick")[classNumber].classList.add("tickOpacity");
    }
    else
    {
        let arrayID = photoIDList.indexOf(id);
        photoIDList.splice(arrayID, 1);
        document.getElementsByClassName("image")[classNumber].classList.remove("imageOpacity");
        document.getElementsByClassName("tick")[classNumber].classList.remove("tickOpacity");
    }
    
}
savePhotos = () => {
    let oldList = [];
    let hiddenData = document.querySelectorAll("input[type='hidden']").length;
    let number;
    for(let i = 0; i < hiddenData; i++){
        number = document.querySelectorAll("input[type='hidden']")[i].value;
        oldList.push(number);   
    }
    $.post(
        "sliders/change",
        {idList: photoIDList, oldId: oldList},
        (data, status) => {
            if(data == "success")
            {
                location.reload();
            }
            else
            {
                alert("Bir sorun oluştu.");
            }
        }
    );
}
homepageChange = (id) => {
    $.post(
        "types/addhomepage",
        {typeId: id},
        (data, status) => {
            if(data == "success")
            {
                location.reload();
            }
            else
            {
                alert("Bir sorun oluştu.");
            }
        }
    )
} 
typeDelete = (id) => {
    $.post(
        "types/deletetype",
        {typeId: id},
        (data, status) => {
            if(data == "success")
            {
                location.reload();
            }
            else
            {
                alert("Bir sorun oluştu.");
            }
        }
    )
}
deletePhoto = (id) => {
    $.post(
        "types/deletephoto",
        {typeId: id},
        (data, status) => {
            if(data == "success")
            {
                location.reload();
            }
            else
            {
                alert("Bir sorun oluşu.");
            }
        }
    );
}
deleteCountryPhoto = (id) => {
    $.post(
        "country/deletephoto",
        {photoID: id},
        (data, status) => {
            if(data == "success")
            {
                location.reload();
            }
            else
            {
                alert("Bir sorun oluştu.");
            }
        }
    );
}
deleteCityPhoto = (id) => {
    $.post(
        "city/deletephoto",
        {photoID: id},
        (data, status) => {
            if(data == "success")
            {
                location.reload();
            }
            else
            {
                alert("Bir sorun oluştu.");
            }
        }
    )   
}
deleteDistrictPhoto = (id) => {
    $.post(
        "district/deletephoto",
        {photoID: id},
        (data, status) => {
            if(data == "success")
            {
                location.reload();
            }
            else
            {
                alert("Bir sorun oluştu.");
            }
        }
    )   
}
deletePlacePhoto = (id) => {
    $.post(
        "places/deletephoto",
        {photoID: id},
        (data, status) => {
            if(data == "success")
            {
                location.reload();
            }
            else
            {
                alert("Bir sorun oluştu.");
            }
        }
    );
}  
deleteCountry = (id) => {
    $.post(
        "country/deletecountry",
        {countryId: id},
        (data, status) => {
            if(data == "success")
            {
                location.reload();
            }
            else
            {
                alert("Bir sorun oluştu.");
            }
        }
    );
}
deleteCity = (id) => {
    $.post(
        "city/deletecity",
        {cityID: id},
        (data, status) => {
            if(data == "success")
            {
                location.reload();
            }
            else
            {
                alert("Bir sorun oluştu.");
            }
        }
    );
}
deleteDistrict = (id) => {
    $.post(
        "district/deletedistrict",
        {districtID: id},
        (data, status) => {
            if(data == "success")
            {
                location.reload();
            }
            else
            {
                alert("Bir sorun oluştu.");
            }
        }
    );
}
deletePlace = (id) => {
    $.post(
        "places/deleteplace",
        {placeID: id},
        (data, status) => {
            if(data == "success")
            {
                location.reload();
            }
            else
            {
                alert("Bir sorun oluştu.");
            }
        }
    );
}
getCountry = () => {
    $.get(
        "city/getCountry",
        (data, status) => {
            data = JSON.parse(data);
            data.forEach(item => document.getElementById("modalCountry").innerHTML +=  `<option value="${item.ID}">${item.Name}</option>`);
        }
    )
}
getCity = () => {
    $.get(
        "district/getCity",
        (data, status) => {
            data = JSON.parse(data);
            data.forEach(item => document.getElementById("modalCity").innerHTML +=  `<option value="${item.ID}">${item.Name}</option>`);
        }
    )
}
getPlaceData = () => {
    $.get(
        "places/getdistrict",
        (data, status) => {
            data = JSON.parse(data);
            data.forEach(item => document.getElementById("modalPlaceDistrict").innerHTML +=  `<option value="${item.ID}">${item.Name}</option>`);
        }
    );
    $.get(
        "places/gettypes",
        (data, status) => {
            data = JSON.parse(data);
            data.forEach(item => document.getElementById("modalPlaceType").innerHTML +=  `<option value="${item.ID}">${item.TypeName}</option>`);
        }
    )
}
$("#cityModal").on('hide.bs.modal', () => {
    document.getElementById("modalCountry").innerHTML = "";
});
$("#placeModal").on('hide.bs.modal', () => {
    document.getElementById("modalPlaceDistrict").innerHTML = "";
});
$("#placeModal").on('hide.bs.modal', () => {
    document.getElementById("modalPlaceType").innerHTML = "";
}); 