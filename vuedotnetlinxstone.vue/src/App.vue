<template>
  <nav class="navbar navbar-dark bg-dark navbar-expand-lg ps-4">
    <!-- <a class="navbar-brand" href="#">Navbar</a> -->
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav"
      aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNav">
      <ul class="navbar-nav">
        <li class="nav-item active">
          <a class="nav-link text-white" href="/">Lista de Produtos</a>
        </li>
      </ul>
    </div>
  </nav>
  <router-view></router-view>
</template>

<script>
export default {
  name: 'App'
};
</script>