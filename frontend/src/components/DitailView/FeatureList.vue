<script>
export default {
  props: {
    featureList: Array,
    compound: String,
    notes: Array,
  },
};
</script>

<template>
  <section style="background: #f8f8f8">
    <div class="container">
      <div class="row">
        <div class="d-none d-lg-block col-lg-1"></div>
        <div class="col-lg-10 py-5">
          <h4 class="mb-4">Характеристики</h4>
          <div class="d-flex align-items-center">
            <div class="compound-title me-3">Состав:</div>
            <div class="card p-2">
              {{ this.compound }}
            </div>
          </div>
          <div class="features-block p-3 mt-5">
            <table
              class="table table-borderless w-100 mb-3"
              v-for="(table, index) in this.featureList"
              :key="index"
            >
              <thead>
                <tr>
                  <th scope="col">
                    <h5>{{ table.title }}</h5>
                  </th>
                  <th scope="col"></th>
                </tr>
              </thead>
              <tbody>
                <tr
                  v-for="(el, index) in table.items"
                  :key="index"
                  style="border-bottom: 1px solid #dfdfdf"
                >
                  <td>
                    {{ el.split("|")[0] }}
                  </td>
                  <td class="d-flex justify-content-end feature-value">
                    {{ el.split("|")[1] }}
                  </td>
                </tr>
              </tbody>
            </table>
            <div class="p-3">
              <div v-for="(el, index) in this.notes" :key="index">
                <div class="notes mb-4">
                  {{ el }}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped>
table {
  background: white;
}
td {
  color: #646464;
}
h4 {
  color: #084158;
}
h5 {
  color: #646464;
}
h4 {
  color: #084158;
}
.compound-title {
  color: #3e798e;
  font-size: 22px;
  font-weight: 500;
}
.features-block {
  background: white;
  box-shadow: 1px 1.732px 10px 0px rgb(0 0 0 / 5%);
}
.card {
  border-radius: 0px;
  border: 0px;
  box-shadow: 1px 1.732px 10px 0px rgb(0 0 0 / 5%);
  width: 100%;
}
.notes {
  color: #646464;
}
.feature-value {
  font-weight: 700;
}
</style>