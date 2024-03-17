<template>
  <div class="container mt-5">
    <div class="card">
      <div class="card-body">
        <div class="d-flex justify-content-between align-items-center mb-3">
          <h3 class="mb-0">Lista de Produtos</h3>
          <router-link to="/create" class="btn btn-primary"><i class="fas fa-add"></i> Novo Produto</router-link>
        </div>
        <div class="input-group mb-3">
          <input type="text" class="form-control" placeholder="Filtrar por nome ou código de barras"
            v-model="globalFilter">
        </div>

        <DataTable :value="filteredProducts" stripedRows removableSort tableStyle="min-width: 50rem"
          table-class="table table-striped" :globalFilter="globalFilter">
          <Column field="image" header="" style="width: 150px" class="align-middle text-center">
            <template #body="slotProps">
              <img :src="`${slotProps.data.image}`" alt="Product Image" width="100">
            </template>
          </Column>
          <Column field="title" header="Produto" style="width: 20%" class="align-middle"></Column>
          <Column field="price" header="Preço" sortable class="align-middle">
            <template #body="slotProps">
              {{ formatCurrency(slotProps.data.price) }}
            </template>
          </Column>
          <Column field="barCode" header="Código de Barras" style="width: 20%" class="align-middle">
            <template #body="slotProps">
              <vue-barcode :value="(slotProps.data.barCode)" :options="{
              format: 'CODE128',
              displayValue: true,
              height: 50, fontSize: 14, marginTop: 0,
              background: 'transparent'
            }" @click="copyBarcode(slotProps.data.barCode)"
                style="cursor: pointer; color: blue; text-decoration: underline;" />
            </template>
          </Column>
          <Column header="" style="width: 20%" class="align-middle text-center">
            <template #body="slotProps">
              <router-link :to="`/edit/${slotProps.data.id}`" class="btn btn-success mx-1">
                <i class="fas fa-edit"></i> Editar
              </router-link>
              <button @click="productDelete(slotProps.data.id)" class="btn btn-danger mx-1">
                <i class="fas fa-trash-alt"></i> Excluir
              </button>
            </template>
          </Column>
        </DataTable>

      </div>
    </div>
  </div>
</template>


<script>
import axios from 'axios';
import Swal from 'sweetalert2';
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';

export default {
  name: 'ProductsList',
  components: {
    DataTable,
    Column,
  },
  data() {
    return {
      products: [],
      globalFilter: ''
    };
  },
  created() {
    this.fetchProducts();
  },
  computed: {
    filteredProducts() {
      if (!this.globalFilter) {
        return this.products;
      }

      const search = this.globalFilter.toLowerCase();
      const startsWithSearch = product =>
        product.title.toLowerCase().startsWith(search) ||
        product.barCode.toLowerCase().startsWith(search);

      return this.products.filter(product =>
        search ? startsWithSearch(product) : true
      );
    }
  },
  methods: {
    fetchProducts() {
      axios.get('/api/products')
        .then(response => {
          this.products = response.data;
          return response;
        })
        .catch(error => {
          return error;
        });
    },
    productDelete(id) {
      Swal.fire({
        title: 'Você tem certeza?',
        text: "Não será possível reverter esta ação!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, tenho certeza!',
        cancelButtonText: 'Cancelar'
      }).then((result) => {
        if (result.isConfirmed) {
          axios.delete(`/api/products/${id}`)
            .then(response => {
              Swal.fire({
                icon: 'success',
                title: 'Produto excluido com sucesso!',
                showConfirmButton: false,
                timer: 1500
              });
              this.fetchProducts();
              return response;
            })
            .catch(error => {
              Swal.fire({
                icon: 'error',
                title: 'Ocorreu um erro!',
                showConfirmButton: false,
                timer: 1500
              });
              return error;
            });
        }
      });
    },
    copyBarcode(barcode) {
      navigator.clipboard.writeText(barcode).then(() => {
        toast("Código de barras copiado com sucesso!", {
          "theme": "colored",
          "type": "success",
          "pauseOnHover": false,
          "pauseOnFocusLoss": false,
          "autoClose": 1000
        });
      }).catch(err => {
        console.error('Erro ao copiar código de barras:', err);
      });
    },
    formatCurrency(value) {
      return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
    }
  },
};
</script>
