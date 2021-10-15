import { ref, watch } from "vue";

export function useImageUpload() {
  const imagefile = ref(null);
  const previewimageurl = ref(null);

  watch(
    () => imagefile.value,
    () => {
      if (imagefile.value != null) {
        let fileReader = new FileReader();
        fileReader.readAsDataURL(imagefile.value);
        fileReader.addEventListener("load", () => {
          previewimageurl.value = fileReader.result;
        });
      }
      previewimageurl.value = null;
    }
  );

  return {
    imagefile,
    previewimageurl,
  };
}
