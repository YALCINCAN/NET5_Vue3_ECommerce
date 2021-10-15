import { ref, watch } from "vue";

export function useMultipleImageUpload() {
  const imagefiles = ref([]);
  const previewimageurls = ref([]);

  watch(
    () => imagefiles.value,
    () => {
      if (imagefiles.value!=null) {
        for (let i = 0; i< imagefiles.value.length; i++) {
          let fileReader = new FileReader();
          fileReader.readAsDataURL(imagefiles.value[i]);
          fileReader.addEventListener("load", () => {
            previewimageurls.value.push(fileReader.result);
          });
        }
      }
      previewimageurls.value=[];
    }
  );

  return {
    imagefiles,
    previewimageurls,
  };
}
