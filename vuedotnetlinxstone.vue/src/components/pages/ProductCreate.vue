<template>
  <div class="container mt-5">
    <div class="card">
      <div class="card-body">
        <div class="d-flex justify-content-between align-items-center mb-3">
          <h3 class="mb-0">Novo Produto</h3>
        </div>
        <form>
          <div class="form-group">
            <label htmlFor="name">Nome</label>
            <input v-model="product.title" type="text" class="form-control" id="name" name="name" />
          </div>
          <div class="form-group">
            <label htmlFor="price">Preço</label>
            <input v-model="product.price" class="form-control" id="price" type="number" name="price" tep="any" />
          </div>
          <div class="form-group">
            <label htmlFor="barCode">Código de barras</label>
            <input v-model="product.barCode" class="form-control" id="barCode" type="text" name="barCode" />
          </div>
          <div class="form-group">
            <label htmlFor="image">Imagem</label>
            <br />
            <img v-if="product.image" :src="product.image" alt="Product Image" width="100">
            <input @change="handleImageChange" class="form-control" id="image" type="file" accept="image/*"
              name="image" />
          </div>
          <button @click="handleSave()" :disabled="isSaving" type="button" class="btn btn-primary mt-3">
            <i class="fas fa-save"></i> Salvar
          </button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import Swal from 'sweetalert2'

export default {
  name: 'ProductCreate',
  data() {
    return {
      product: {
        title: '',
        price: '',
        barCode: '',
        image: null,
      },
      isSaving: false,
    };
  },
  methods: {
    handleSave() {
      this.isSaving = true;
      axios.post('/api/products', this.product)
        .then(response => {
          Swal.fire({
            icon: 'success',
            title: 'Produto salvo com sucesso!',
            showConfirmButton: false,
            timer: 1500
          }).then(() => {
            this.$router.push('/');
          });
          this.isSaving = false;
          return response;
        })
        .catch(error => {
          this.isSaving = false;
          Swal.fire({
            icon: 'error',
            title: 'An Error Occured!',
            showConfirmButton: false,
            timer: 1500
          });
          return error;
        });
    },
    handleImageChange(event) {
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        this.product.image = reader.result;
      };
      reader.readAsDataURL(file);
    }
  },
};
</script>
