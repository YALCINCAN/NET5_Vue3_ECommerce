import { exportFile } from "quasar";

export function useExportCsv(data, columns, foldername) {
  function wrapCsvValue(val, formatFn) {
    let formatted = formatFn !== void 0 ? formatFn(val) : val;
    formatted =
      formatted === void 0 || formatted === null ? "" : String(formatted);
    formatted = formatted.split('"').join('""');
    return `"${formatted}"`;
  }
  
  function exportTable() {
    // naive encoding to csv format
    const content = [columns.map((col) => wrapCsvValue(col.label))]
      .concat(
        data.value.map((row) =>
          columns
            .map((col) =>
              wrapCsvValue(
                typeof col.field === "function"
                  ? col.field(row)
                  : row[col.field === void 0 ? col.name : col.field],
                col.format
              )
            )
            .join(",")
        )
      )
      .join("\r\n");
    const status = exportFile(foldername + ".csv", content, "text/csv");
    if (status !== true) {
      $q.notify({
        message: "Browser denied file download...",
        color: "negative",
        icon: "warning",
      });
    }
  }

  return {
    exportTable,
  };
}
