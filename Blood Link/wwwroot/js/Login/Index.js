$(function () {
    $('.login-info-box').fadeOut();
    $('.login-show').addClass('show-log-panel');

    $('.login-reg-panel input[type="radio"]').on('change', function () {
        if ($('#log-login-show').is(':checked')) {
            // Show register panel
            $('.register-info-box').fadeOut();
            $('.login-info-box').fadeIn();
            $('.white-panel').addClass('right-log');
            $('.register-show').addClass('show-log-panel');
            $('.login-show').removeClass('show-log-panel');
        } else if ($('#log-reg-show').is(':checked')) {
            // Show login panel
            $('.register-info-box').fadeIn();
            $('.login-info-box').fadeOut();
            $('.white-panel').removeClass('right-log');
            $('.login-show').addClass('show-log-panel');
            $('.register-show').removeClass('show-log-panel');
        }
    });

    //$('#connect-wallet').click(async function (event) { 
    //    event.preventDefault();
    //    // Check if Phantom wallet is available
    //    if (window.solana && window.solana.isPhantom) {
    //        console.log("Phantom wallet found!");

    //        try {
    //            // Connect to the wallet
    //            const response = await window.solana.connect();
    //            console.log('Wallet public key:', response.publicKey.toString());

    //            // You can now interact with Solana Web3.js using the connected wallet
    //            const connection = new solanaWeb3.Connection(
    //                solanaWeb3.clusterApiUrl('devnet'), 'confirmed'
    //            );
    //            console.log('Connected to Solana devnet');

    //            // Further Web3 interactions using the connected wallet can be done here

    //        } catch (err) {
    //            console.error('Error connecting to Phantom wallet:', err);
    //        }
    //    } else {
    //        alert('Phantom wallet not found. Please install Phantom wallet extension.');
    //    }
    //});
});