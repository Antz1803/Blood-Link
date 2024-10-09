$(document).ready(function () {
    if (window.solana && window.solana.isPhantom) {
        console.log("Phantom wallet found!");

        // Connect to the wallet
        window.solana.connect().then(response => {
            const publicKey = response.publicKey.toString();
            console.log('Phantom wallet public key:', publicKey);

            // Call your backend to check the Phantom login
            $.ajax({
                url: '/Wallet/CheckPhantomLogin',  // Your controller action URL
                type: 'POST',
                data: { walletPublicKey: publicKey },
                success: function (response) {
                    if (response.success) {
                        $('#link-wallet').text("Remove Wallet");
                    } else {
                        console.log("Failed to link Phantom wallet.");
                    }
                },
                error: function (error) {
                    console.error("Error during Phantom wallet login:", error);
                }
            });
        }).catch(error => {
            console.error("Error connecting to Phantom wallet:", error);
        });
    } else {
        alert('Phantom wallet not found. Please install Phantom wallet extension.');
    }
});