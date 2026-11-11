const nextStepBtn = document.querySelectorAll(".nextStepFeature");
const tabs = document.querySelectorAll(".stepsTab");
const tabsContent = document.querySelectorAll(".featureStepsContent");
nextActive = "";
currentItem = "";
nextStepBtn.forEach((nextStep,i)=>{
    nextStep.addEventListener("click", (event) => {
        tabs.forEach((item, i) => {
          if (item.classList.contains("active") && i < tabs.length) {
            currentItem = item;
            nextItem=currentItem.nextElementSibling;
          }
        });
        tabsContent.forEach((item,i)=>{
          if (item.classList.contains("active") && i < tabs.length) {
              currentItemContent = item;
              nextItemContent=currentItemContent.nextElementSibling;
            }
        })
        currentItem.classList.remove("active");
        currentItem.classList.remove("show");
        currentItemContent.classList.remove("active");
        currentItemContent.classList.remove("show");
      
        if (nextItem.classList.contains("stepsTab") && nextItem!=null) {
          nextItem.classList.add("active", "show");
          nextItemContent.classList.add("active", "show");
      
        }else if(nextItem.nextElementSibling.classList.contains("stepsTab") && nextItem.nextElementSibling!=null){
          nextItem.nextElementSibling.classList.add("active", "show");
          nextItemContent.classList.add("active", "show");
        }
      
        window.scrollTo({
          top: currentItemContent.offsetTop,
          left: 0,
          behavior: "smooth",
        });
      });
})
