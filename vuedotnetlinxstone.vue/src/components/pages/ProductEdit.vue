<template>
  <div class="container mt-5">
    <div class="card">
      <div class="card-body">
        <div class="d-flex justify-content-between align-items-center mb-3">
          <h3 class="mb-0">Editar Produto</h3>
        </div>
        <form @submit.prevent="handleSave">
          <input v-model="product.id" type="text" class="form-control d-none" id="id" name="id" />
          <div class="form-group">
            <label for="title">Nome</label>
            <input v-model="product.title" type="text" class="form-control" id="title" name="title" />
          </div>
          <div class="form-group">
            <label for="price">Preço</label>
            <input v-model="product.price" class="form-control" id="price" rows="3" name="price" type="number"
              step="any" />
          </div>
          <div class="form-group">
            <label for="barCode">Código de Barras</label>
            <input v-model="product.barCode" class="form-control" id="barCode" name="barCode" type="text" />
          </div>
          <div class="form-group">
            <label for="image">Imagem</label>
            <br />
            <img v-if="product.image" :src="product.image" alt="Product Image" width="100">
            <input @change="handleImageChange" class="form-control" id="image" name="image" accept="image/*"
              type="file" />
          </div>
          <button :disabled="isSaving" type="submit" class="btn btn-primary mt-3">
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

  name: 'ProductEdit',
  data() {
    return {
      product: {
        id: 0,
        title: '',
        price: 0,
        barCode: '',
        image: null,
      },
      imageUrl: null,
      isSaving: false,
    };
  },
  created() {
    const id = this.$route.params.id;
    axios.get(`/api/products/${id}`)
      .then(response => {
        let product = response.data
        this.product.id = product.id
        this.product.title = product.title
        this.product.price = parseFloat(product.price)
        this.product.barCode = product.barCode
        this.product.image = product.image
        return response
      })
      .catch(error => {
        Swal.fire({
          icon: 'error',
          title: 'An Error Occured!',
          showConfirmButton: false,
          timer: 1500
        })
        return error
      })
  },
  methods: {
    handleSave() {
      this.isSaving = true
      const id = this.$route.params.id;
      axios.put(`/api/products/${id}`, this.product)
        .then(response => {
          Swal.fire({
            icon: 'success',
            title: 'Produto atualizado com sucesso!',
            showConfirmButton: false,
            timer: 1500
          }).then(() => {
            this.$router.push('/');
          });
          this.isSaving = false
          return response
        })
        .catch(error => {
          this.isSaving = false
          Swal.fire({
            icon: 'error',
            title: 'An Error Occured!',
            showConfirmButton: false,
            timer: 1500
          })
          return error
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