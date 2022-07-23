<script>
import CheckMark from "../icons/CheckMark.vue";

export default {
  components: {
    CheckMark,
  },
  props: {
    areasOfUse: Array,
  },
};
</script>

<template>
  <section style="background: #f8f8f8">
    <div class="container">
      <div class="row">
        <div class="d-none d-lg-block col-lg-1"></div>
        <div class="col-lg-10 py-5">
          <h4>Область применения</h4>
          <div class="areasOfUse-block p-3 mt-5">
            <table
              class="table table-borderless w-100 mb-5"
              v-for="(table, index) in this.areasOfUse"
              :key="index"
            >
              <thead>
                <tr>
                  <th scope="col">
                    <h5>{{ table.title }}</h5>
                  </th>
                  <th scope="col d-flex">
                    <div class="float-end"><h5>Без ограничения</h5></div>
                  </th>
                </tr>
              </thead>
              <tbody>
                <tr
                  v-for="(el, index) in table.items"
                  :key="index"
                  style="border-bottom: 1px solid #dfdfdf"
                >
                  <td>
                    {{ el }}
                  </td>
                  <td class="d-flex justify-content-end">
                    <CheckMark class="me-5 pe-4" />
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style scooed>
td {
  color: #646464;
}
td svg {
  height: 20px;
  fill: #646464;
}
h4 {
  color: #084158;
}
h5 {
  color: #646464;
}
.areasOfUse-block {
  background: white;
  box-shadow: 1px 1.732px 10px 0px rgb(0 0 0 / 5%);
}
</style>